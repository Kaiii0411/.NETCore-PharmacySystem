using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacySystem.Models.ViewModels;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IRolesApiClient _roleApiClient;
        private readonly IStaffApiClient _staffApiClient;
        public UsersController(IUserApiClient userApiClient,
            IRolesApiClient roleApiClient,
            IConfiguration configuration,
            IStaffApiClient staffApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _roleApiClient = roleApiClient;
            _staffApiClient = staffApiClient;
        }
        public async Task<IActionResult> Index(string? UserName, long? IdAccount)
        {
            var request = new GetUsersPagingRequest()
            {
                UserName = UserName,
                IdAccount = IdAccount
            };
            var data = await _userApiClient.Get(request);
            List<SelectListItem> listAccount = new List<SelectListItem>
            {
                new SelectListItem{Text = "Admin", Value = "1", Selected = IdAccount == 1},
                new SelectListItem{Text = "Supplier", Value = "2", Selected = IdAccount == 2},
                new SelectListItem{Text = "Store Owner", Value = "3", Selected = IdAccount == 3},
                new SelectListItem{Text = "Staff", Value = "4", Selected = IdAccount == 4},
            };
            ViewBag.ListAccount = listAccount;
            ViewBag.UserName = UserName;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.ResultObj);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var staffList = await _staffApiClient.GetListStaff();
            List<ListAccountVM> listAccounts = new List<ListAccountVM>
            {
                new ListAccountVM {IdAccount = 1, Description = "Admin"},
                new ListAccountVM {IdAccount = 2, Description = "Supplier"},
                new ListAccountVM {IdAccount = 3, Description = "Store Owner"},
                new ListAccountVM {IdAccount = 4, Description = "Staff"},
            };
            ViewBag.ListOfStaff = staffList;
            ViewBag.ListAccounts = listAccounts;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userApiClient.RegisterUser(request);
                if (result.IsSuccessed)
                {
                    TempData["result"] = "New user successfully added!";
                    return Json(0);
                }
                ModelState.AddModelError("", result.Message);
            }
            return Json(1);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                List<SelectListItem> listAccount = new List<SelectListItem>
                {
                    new SelectListItem{Text = "Admin", Value = "1", Selected = user.IdAccount == 1},
                    new SelectListItem{Text = "Supplier", Value = "2", Selected = user.IdAccount == 2},
                    new SelectListItem{Text = "Store Owner", Value = "3", Selected = user.IdAccount == 3},
                    new SelectListItem{Text = "Staff", Value = "4", Selected = user.IdAccount == 4},
                };
                var staffList = await _staffApiClient.GetListStaff();
                ViewBag.ListOfStaff = staffList.Select(x => new SelectListItem()
                {
                    Text = x.StaffName,
                    Value = x.IdStaff.ToString(),
                    Selected = user.IdStaff == x.IdStaff 
                });
                ViewBag.ListAccount = listAccount;
                var updateRequest = new UserUpdateRequest()
                {
                    Id = id,
                    IdAccount = user.IdAccount,
                    IdStaff = user.IdStaff
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UserUpdateRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userApiClient.UpdateUser(request.Id, request);
                if (result.IsSuccessed)
                {
                    TempData["result"] = "User update successful!";
                    return Json(0);
                }
                ModelState.AddModelError("", result.Message);
            }
            return Json(1);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                List<SelectListItem> listAccount = new List<SelectListItem>
                {
                    new SelectListItem{Text = "Admin", Value = "1", Selected = user.IdAccount == 1},
                    new SelectListItem{Text = "Supplier", Value = "2", Selected = user.IdAccount == 2},
                    new SelectListItem{Text = "Store Owner", Value = "3", Selected = user.IdAccount == 3},
                    new SelectListItem{Text = "Staff", Value = "4", Selected = user.IdAccount == 4},
                };
                var staffList = await _staffApiClient.GetListStaff();
                ViewBag.ListOfStaff = staffList.Select(x => new SelectListItem()
                {
                    Text = x.StaffName,
                    Value = x.IdStaff.ToString(),
                    Selected = user.IdStaff == x.IdStaff
                });
                ViewBag.ListAccount = listAccount;
                return View(new UserDeleteRequest()
                {
                    Id = id,
                    IdAccount = user.IdAccount,
                    IdStaff = user.IdStaff
                });
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Delete(request.Id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Delete user successfully!";
                return Json(new { result = "Redirect", url = Url.Action("Index", "Users") });
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RoleAssign(request.Id, request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);

            return View(roleAssignRequest);
        }
        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObj = await _userApiClient.GetById(id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObj.ResultObj.Roles.Contains(role.Name)
                });
            }
            return roleAssignRequest;
        }
    }
}
