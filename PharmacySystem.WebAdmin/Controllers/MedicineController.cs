using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models.Request;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineApiClient _medicineApiClient;
        private readonly IMedicineGroupApiClient _medicineGroupApiClient;
        private readonly IConfiguration _configuration;

        public MedicineController(IMedicineApiClient medicineApiClient, IConfiguration configuration, IMedicineGroupApiClient medicineGroupApiClient)
        {
            this._configuration = configuration;
            this._medicineApiClient = medicineApiClient;
            this._medicineGroupApiClient = medicineGroupApiClient;
        }
        public IActionResult Index()
        {          
            return View();          
        }
        public async Task<IActionResult> Create()
        {
            var medicineGroupList = await _medicineGroupApiClient.GetListMedicineGroup();
            ViewBag.ListOfMedicineGroup = medicineGroupList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MedicineCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _medicineApiClient.CreateMedicine(request);
            if (result)
            {
                TempData["result"] = "Successfully added new medicine!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to add new medicine");
            return View(request);
        }
        public IActionResult Edit()
        {
            return View();
        }
    }
}
