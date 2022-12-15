using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacySystem.Models;
using PharmacySystem.Models.ViewModels;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/invoicedetails")]
    [ApiController]
    public class InvoiceDetailsController : Controller
    {
        private readonly IInvoiceDetailsService _invoiceDetailsService;
        public InvoiceDetailsController(IInvoiceDetailsService invoiceDetailsService)
        {
            this._invoiceDetailsService = invoiceDetailsService;
        }
    }
}
