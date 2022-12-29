using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.Models;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Identity;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Service
{
    public interface IRolesService
    {
        Task<List<RoleVM>> GetAll();
        Task<PagedResult<RoleVM>> Get(GetRolesPagingRequest request);
        Task<ApiResult<bool>> Create(RoleCreateRequest request);
        Task<ApiResult<RolesVM>> GetById(Guid id);
        Task<ApiResult<bool>> Update(Guid id, RoleUpdateRequest request);
        Task<List<UsersRoleVM>> GetUsersByRoles(Guid id);
        Task<ApiResult<bool>> EditUsersInRoles(Guid idRole, UserAssignRequest model);
    }
    public class RolesService: IRolesService
    {
        private readonly RoleManager<Roles> _roleManager;
        private readonly UserManager<Users> _userManager;

        public RolesService(RoleManager<Roles> roleManager, UserManager<Users> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }   
        public async Task<List<RoleVM>> GetAll()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();
            return roles;
        }
        public async Task<PagedResult<RoleVM>> Get(GetRolesPagingRequest request)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(request.RoleName))
            {
                query = query.Where(x => x.Name.Contains(request.RoleName));
            }
            int totalRow = await query.CountAsync();

            var data = await query.Select(x => new RoleVM()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
            //data
            var pagedResult = new PagedResult<RoleVM>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
        public async Task<ApiResult<bool>> Create(RoleCreateRequest request)
        {
            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role != null)
            {
                return new ApiErrorResult<bool>("Role already exists!");
            }
            role = new Roles()
            {
                Name = request.RoleName,
                Description = request.Description
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Create failed!");
        }
        public async Task<ApiResult<RolesVM>> GetById(Guid id)
        {
            var role= await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return new ApiErrorResult<RolesVM>("Role does not exist!");
            }
            var model = new RolesVM()
            {
                Id=id,
                RoleName = role.Name,
                Description=role.Description
            };
            foreach(var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return new ApiSuccessResult<RolesVM>(model);
        }
        public async Task<ApiResult<bool>> Update(Guid id, RoleUpdateRequest request)
        {
            var roles = await _roleManager.FindByIdAsync(id.ToString());
            roles.Name = request.RoleName;
            roles.Description = request.Description;
            var result = await _roleManager.UpdateAsync(roles);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Update failed!");
        }
        public async Task<List<UsersRoleVM>> GetUsersByRoles(Guid id)
        {
            var roles = await _roleManager.FindByIdAsync(id.ToString());
            var model = new List<UsersRoleVM>();
            foreach(var user in _userManager.Users)
            {
                var userVM = new UsersRoleVM
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if(await _userManager.IsInRoleAsync(user, roles.Name))
                {
                    userVM.IsSelected = true;
                }
                else
                {
                    userVM.IsSelected = false;
                }
                model.Add(userVM);
            }
            return model;
        }
        public async Task<ApiResult<bool>> EditUsersInRoles(Guid idRole, UserAssignRequest model)
        {
            var roles = await _roleManager.FindByIdAsync(idRole.ToString());
            if (roles == null)
            {
                return new ApiErrorResult<bool>("Role does not exist!");
            }
            for(int i = 0; i < model.Users.Count; i++ )
            {
                var user = await _userManager.FindByIdAsync(model.Users[i].Id.ToString());
                IdentityResult result = null;
                if (model.Users[i].Selected && await _userManager.IsInRoleAsync(user, roles.Name) == false)
                {
                    result = await _userManager.AddToRoleAsync(user, roles.Name);
                }
                else if (!model.Users[i].Selected && await _userManager.IsInRoleAsync(user, roles.Name) == true)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, roles.Name);
                }
                else
                {
                    continue;
                }

                if(result.Succeeded)
                {
                    if (i < (model.Users.Count - 1))
                        continue;
                    else
                        return new ApiSuccessResult<bool>();
                }
            }
            return new ApiErrorResult<bool>("Update failed!");
        }
    }
}
