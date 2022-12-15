using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models.Request;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class SupplierGroupController : Controller
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
    }
}
