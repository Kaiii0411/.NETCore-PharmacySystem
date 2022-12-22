using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacySystem.Models.ViewModels;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Web.Helpers;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierApiClient _supplierApiClient;
        private readonly ISupplierGroupApiClient _supplierGroupApiClient;
        private readonly IConfiguration _configuration;

        public SupplierController(ISupplierApiClient supplierApiClient
            , IConfiguration configuration
            , ISupplierGroupApiClient supplierGroupApiClient)
        {
            this._supplierApiClient = supplierApiClient;
            this._configuration = configuration;
            this._supplierGroupApiClient = supplierGroupApiClient;
        }
        //Index
        public async Task<IActionResult> Index(string? Keyword, long? IdSupplierGroup)
        {
            var request = new GetManageSupplierPagingRequest()
            {
                Keyword = Keyword,
                IdSupplierGroup = IdSupplierGroup
            };
            var data = await _supplierApiClient.Get(request);
            ViewBag.Keyword = Keyword;

            var supplierGroupList = await _supplierGroupApiClient.GetListSupplierGroup();
            ViewBag.ListOfSupplierGroup = supplierGroupList.Select(x => new SelectListItem()
            {
                Text = x.SupplierGroupName,
                Value = x.IdSupplierGroup.ToString(),
                Selected = IdSupplierGroup.HasValue && IdSupplierGroup.Value == x.IdSupplierGroup
            });
            return View(data);
        }
        //Create
        public async Task<IActionResult> Create()
        {
            var supplierGroupList = await _supplierGroupApiClient.GetListSupplierGroup();
            ViewBag.ListOfSupplierGroup = supplierGroupList;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(SupplierCreateRequest CreateSupplierForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _supplierApiClient.CreateSupplier(CreateSupplierForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var supplier = await _supplierApiClient.GetById(id);
            var details = new SupplierUpdateRequest()
            {
                IdSupplier = supplier.IdSupplier,
                SupplierName = supplier.SupplierName,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                IdSupplierGroup = supplier.IdSupplierGroup
            };
            var supplierGroupList = await _supplierGroupApiClient.GetListSupplierGroup();
            ViewBag.ListOfSupplier = supplierGroupList.Select(x => new SelectListItem()
            {
                Text = x.SupplierGroupName,
                Value = x.IdSupplierGroup.ToString(),
                Selected = supplier.IdSupplierGroup == x.IdSupplierGroup
            });
            return View(details);
        }
        [HttpPut]
        public async Task<JsonResult> Edit([FromForm] SupplierUpdateRequest UpdateSupplierForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _supplierApiClient.UpdateSupplier(UpdateSupplierForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var supplier = await _supplierApiClient.GetById(id);
            var supplierGroupList = await _supplierGroupApiClient.GetListSupplierGroup();
            ViewBag.ListOfSupplier = supplierGroupList.Select(x => new SelectListItem()
            {
                Text = x.SupplierGroupName,
                Value = x.IdSupplierGroup.ToString(),
                Selected = supplier.IdSupplierGroup == x.IdSupplierGroup
            });
            return View(new SupplierDeleteRequest()
            {
                IdSupplier = supplier.IdSupplier,
                SupplierName = supplier.SupplierName,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                IdSupplierGroup = supplier.IdSupplierGroup
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(SupplierDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _supplierApiClient.DeleteSupplier(request.IdSupplier);

            if (result)
            {
                return Json(new { result = "Redirect", url = Url.Action("Index", "Supplier") });
            }
            return View(request);
        }
    }
}
