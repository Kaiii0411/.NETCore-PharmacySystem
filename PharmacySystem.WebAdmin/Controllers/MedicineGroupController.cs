using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models.Request;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class MedicineGroupController : Controller
    {
        private readonly IMedicineGroupApiClient _medicineGroupApiClient;
        private readonly IConfiguration _configuration;
        public MedicineGroupController(IConfiguration configuration, IMedicineGroupApiClient medicineGroupApiClient)
        {
            this._configuration = configuration;
            this._medicineGroupApiClient = medicineGroupApiClient;
        }
        public async Task<IActionResult> Index(string? Keyword)
        {
            var request = new GetManageKeywordPagingRequest()
            {
                Keyword = Keyword
            };
            var data = await _medicineGroupApiClient.Get(request);
            ViewBag.Keyword = Keyword;
            return View(data);
        }
        [HttpPost]
        public async Task<JsonResult> Create(MedicineGroupCreateRequest CreateMedicineGroupForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _medicineGroupApiClient.CreateMedicineGroup(CreateMedicineGroupForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
    }
}
