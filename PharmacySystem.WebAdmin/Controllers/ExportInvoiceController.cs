using Microsoft.AspNetCore.Mvc;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class ExportInvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
