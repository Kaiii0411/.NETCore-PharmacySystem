using Microsoft.AspNetCore.Mvc;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
