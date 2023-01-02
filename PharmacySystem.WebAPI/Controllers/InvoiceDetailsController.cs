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
        [HttpGet("detailsbymedicine/{medicineId}")]
        public async Task<IActionResult> GetByIdMedicine(long medicineId)
        {
            var details = await _invoiceDetailsService.GetListDeatilsByIdMedicine(medicineId);
            if (details == null)
                return BadRequest("Cannot find details");
            return Ok(details);
        }
        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenue()
        {
            var revenue = await _invoiceDetailsService.GetRevenue();
            if (revenue == null)
                return BadRequest("Error to calculate!");
            return Ok(revenue);
        }
    }
}
