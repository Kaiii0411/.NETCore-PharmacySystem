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
        public IActionResult Index()
        {
            return View();
        }
    }
}
