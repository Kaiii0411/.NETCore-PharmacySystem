using AspNetCore.Report.ReportService2010_;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;

namespace PharmacySystem.WebAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IRolesApiClient _roleApiClient;
        private readonly IUserApiClient _userApiClient;

        public RolesController(IRolesApiClient roleApiClient,
            IConfiguration configuration,
            IUserApiClient userApiClient)
        {
            _configuration = configuration;
            _roleApiClient = roleApiClient;
            _userApiClient = userApiClient;
        }
        public async Task <IActionResult> Index(string? RoleName)
        {
            var request = new GetRolesPagingRequest()
            {
                RoleName = RoleName
            };
            ViewBag.RoleName = RoleName;
            var data = await _roleApiClient.Get(request);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _roleApiClient.GetById(id);
            return View(result.ResultObj);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(RoleCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleApiClient.Create(request);
                if (result.IsSuccessed)
                {
                    TempData["result"] = "New role successfully added!";
                    return Json(0);
                }
                ModelState.AddModelError("", result.Message);
            }
            return Json(1);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _roleApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var users = await _roleApiClient.GetUsersByRoles(id);

                var updateRequest = new RoleUpdateRequest()
                {
                    Id = id,
                    RoleName = result.ResultObj.RoleName,
                };
                foreach(var item in users.ResultObj)
                {
                    updateRequest.Users.Add(item.UserName);
                }

                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<JsonResult> Edit(RoleUpdateRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleApiClient.Update(request.Id, request);
                if (result.IsSuccessed)
                {
                    TempData["result"] = "User update successful!";
                    return Json(0);
                }
                ModelState.AddModelError("", result.Message);
            }
            return Json(1);
        }
        [HttpGet]
        public async Task<IActionResult> UserAssign(Guid id)
        {
            ViewBag.RoleId = id;
            var roleAssignRequest = await GetUserAssignRequest(id);
            return View(roleAssignRequest);
        }
        [HttpPost]
        public async Task<IActionResult>UserAssign(UserAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roleApiClient.EditUsersInRoles(request.Id, request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetUserAssignRequest(request.Id);

            return View(roleAssignRequest);
        }
        private async Task<UserAssignRequest> GetUserAssignRequest(Guid id)
        {
            var roleObj = await _roleApiClient.GetById(id);
            var userObj = await _userApiClient.GetAll();
            var userAssignRequest = new UserAssignRequest();
            foreach (var user in userObj.ResultObj)
            {
                userAssignRequest.Users.Add(new SelectItem()
                {
                    Id = user.Id.ToString(),
                    Name = user.UserName,
                    Selected = roleObj.ResultObj.Users.Contains(user.UserName)
                });
            }
            return userAssignRequest;
        }
    }
}
