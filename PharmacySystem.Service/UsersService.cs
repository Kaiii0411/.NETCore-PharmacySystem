using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PharmacySystem.Models;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Identity;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Service
{
    public interface IUsersService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<ApiResult<UsersVM>> GetById(Guid id);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<PagedResult<UsersVM>> Get(GetUsersPagingRequest request);
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);
        Task<ApiResult<bool>> Delete(Guid id);
        Task<List<UsersVM>> GetAll();
    }
    public class UsersService : IUsersService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IConfiguration _config;
        private readonly IStaffService _staffService;

        public UsersService(UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            RoleManager<Roles> roleManager,
            IConfiguration config,
            IStaffService staffService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _staffService = staffService;
        }
        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Account does not exist!");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Incorrect login!");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Account already exists!");
            }

            user = new Users()
            {
                UserName = request.UserName,
                IdStaff = request.IdStaff,
                IdAccount = request.IdAccount
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Registration failed!");
        }
        public async Task<List<UsersVM>> GetAll()
        {
            var users = await _userManager.Users
                .Select(x => new UsersVM()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    IdAccount = x.IdAccount,
                    IdStaff = x.IdStaff
                }).ToListAsync();
            return users;
        }
        public async Task<PagedResult<UsersVM>> Get(GetUsersPagingRequest request)
        {
            var query = _userManager.Users;
            if(!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(x => x.UserName.Contains(request.UserName));
            }
            if (request.IdAccount != null && request.IdAccount != 0)
            {
                query = query.Where(x => x.IdAccount == request.IdAccount);
            }
            int totalRow = await query.CountAsync();

            var data = await query.Select(x => new UsersVM()
            {
                Id = x.Id,
                IdAccount = x.IdAccount,
                IdStaff = x.IdStaff,
                UserName = x.UserName
            }).ToListAsync();

            //data
            var pagedResult = new PagedResult<UsersVM>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
        public async Task<ApiResult<UsersVM>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UsersVM>("User does not exist!");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UsersVM()
            {
                Id = user.Id,
                IdAccount = user.IdAccount,
                IdStaff = user.IdStaff,
                UserName = user.UserName,
                Roles = roles
            };
            return new ApiSuccessResult<UsersVM>(userVm);
        }
        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Account does not exist!");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>();
        }
        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.IdStaff = request.IdStaff;
            user.IdAccount = request.IdAccount;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Update failed!");
        }
        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User does not exist!");
            }
            var reult = await _userManager.DeleteAsync(user);
            if (reult.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Deletion failed!");
        }
    }
}
