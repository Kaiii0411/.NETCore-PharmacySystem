using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    }
}
