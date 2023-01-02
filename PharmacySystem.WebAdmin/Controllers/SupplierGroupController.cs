using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;

namespace PharmacySystem.WebAdmin.Controllers
{
    [Authorize(Roles = "Admin,StoreOwner")]
    public class SupplierGroupController : BaseController
    {
        private readonly ISupplierGroupApiClient _supplierGroupApiClient;
        private readonly IConfiguration _configuration;
        public SupplierGroupController(IConfiguration configuration, ISupplierGroupApiClient supplierGroupApiClient)
        {
            this._configuration = configuration;
            this._supplierGroupApiClient = supplierGroupApiClient;
        }
        public async Task<IActionResult> Index(string? Keyword)
        {
            var request = new GetManageKeywordPagingRequest()
            {
                Keyword = Keyword
            };
            var data = await _supplierGroupApiClient.Get(request);
            ViewBag.Keyword = Keyword;
            return View(data);
        }
        [HttpPost]
        public async Task<JsonResult> Create(SupplierGroupCreateRequest CreateSupplierGroupForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _supplierGroupApiClient.CreateSupplierGroup(CreateSupplierGroupForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var supplierGroup = await _supplierGroupApiClient.GetById(id);
            var details = new SupplierGroupUpdateRequest()
            {
                IdSupplierGroup = supplierGroup.IdSupplierGroup,
                SupplierGroupName = supplierGroup.SupplierGroupName,
                Note = supplierGroup.Note
            };
            return View(details);
        }
        [HttpPut]
        public async Task<JsonResult> Edit([FromForm] SupplierGroupUpdateRequest UpdateSupplierGroupForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _supplierGroupApiClient.UpdateSupplierGroup(UpdateSupplierGroupForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var supplierGroup = await _supplierGroupApiClient.GetById(id);
            return View(new SupplierGroupDeleteRequest()
            {
                IdSupplierGroup = supplierGroup.IdSupplierGroup,
                SupplierGroupName = supplierGroup.SupplierGroupName,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(SupplierGroupDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _supplierGroupApiClient.DeleteSupplierGroup(request.IdSupplierGroup);

            if (result)
            {
                return Json(new { result = "Redirect", url = Url.Action("Index", "SupplierGroup") });
            }
            return View(request);
        }
    }
}
