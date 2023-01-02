using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PharmacySystem.WebAdmin.Controllers
{
    [Authorize(Roles = "Admin,StoreOwner")]
    public class StaffController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IStaffApiClient _staffApiClient;
        private readonly IStoreApiClient _storeApiClient;
        public StaffController(IConfiguration configuration, IStaffApiClient staffApiClient, IStoreApiClient storeApiClient)
        {
            this._configuration = configuration;
            this._staffApiClient = staffApiClient;
            this._storeApiClient = storeApiClient;
        }
        public async Task<IActionResult> Index(string? StaffName, long? IdStaff, long? IdStore)
        {
            var request = new GetManageStaffPagingRequest()
            {
                StaffName = StaffName,
                IdStaff = IdStaff,
                IdStore = IdStore
            };
            var data = await _staffApiClient.Get(request);
            ViewBag.StaffName = StaffName;
            ViewBag.IdStaff = IdStaff;
            var storelist = await _storeApiClient.GetListStore();
            ViewBag.ListOfStore = storelist.Select(x => new SelectListItem()
            {
                Text = x.StoreName,
                Value = x.IdStore.ToString(),
                Selected = IdStore.HasValue && IdStore.Value == x.IdStore
            });
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            var storelist = await _storeApiClient.GetListStore();
            ViewBag.ListOfStore = storelist;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(StaffCreateRequest CreateStaffForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _staffApiClient.CreateStaff(CreateStaffForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var staff = await _staffApiClient.GetById(id);
            var storelist = await _storeApiClient.GetListStore();
            ViewBag.ListOfStore = storelist.Select(x => new SelectListItem()
            {
                Text = x.StoreName,
                Value = x.IdStore.ToString(),
                Selected = staff.IdStore == x.IdStore
            });
            var details = new StaffUpdateRequest()
            {
                IdStaff = id,
                StaffName = staff.StaffName,
                DateOfBirth = staff.DateOfBirth,
                Phone = staff.Phone,
                Email = staff.Email,
                IdStore = staff.IdStore,
            };
            return View(details);
        }
        [HttpPut]
        public async Task<JsonResult> Edit([FromForm] StaffUpdateRequest UpdateStaffForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _staffApiClient.UpdateStaff(UpdateStaffForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var staff = await _staffApiClient.GetById(id);
            var storelist = await _storeApiClient.GetListStore();
            ViewBag.ListOfStore = storelist.Select(x => new SelectListItem()
            {
                Text = x.StoreName,
                Value = x.IdStore.ToString(),
                Selected = staff.IdStore == x.IdStore
            });
            return View(new StaffDeleteRequest()
            {
                IdStaff = id,
                StaffName = staff.StaffName,
                DateOfBirth = staff.DateOfBirth,
                Phone = staff.Phone,
                Email = staff.Email,
                IdStore = staff.IdStore,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(StaffDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _staffApiClient.DeleteStaff(request.IdStaff);

            if (result)
            {
                return Json(new { result = "Redirect", url = Url.Action("Index", "Staff") });
            }
            return View(request);
        }
    }
}
