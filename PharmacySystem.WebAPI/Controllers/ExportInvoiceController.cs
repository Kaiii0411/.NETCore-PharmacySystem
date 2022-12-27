using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using PharmacySystem.Service;
using PharmacySystem.WebAPI.Models;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/exportinvoices")]
    [ApiController]
    public class ExportInvoiceController : Controller
    {
        private readonly IInvoiceService _InvoiceService;
        private readonly PharmacySystemContext _context;
        //crud
        public ExportInvoiceController(PharmacySystemContext context, IInvoiceService InvoiceService)
        {
            this._context = context;
            this._InvoiceService = InvoiceService;
        }
        [HttpPost("create")]
        public async Task<RequestResponse> Create(ExportInvoiceCreateRequest request)
        {
            var InvoiceId = await _InvoiceService.AddExportInvoice(request);
            if (InvoiceId == 0)
            {
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Message = "Add Failed!"
                };
            }
            return new RequestResponse
            {
                StatusCode = Code.Success,
                Message = "Add sucess!"
            };
        }
        [HttpDelete("delete/{invoiceId}")]
        public async Task<RequestResponse> Delete(long invoiceId)
        {
            var medicine = await _InvoiceService.DeleteExportInvoice(invoiceId);
            if (medicine == 0)
            {
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Message = $"Delete Failed! Can notfind a invoice: {invoiceId}"
                };
            }
            return new RequestResponse
            {
                StatusCode = Code.Success,
                Message = "Delete sucess!"
            };
        }
        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetManageEInvoicePagingRequest request)
        {
            var invoices = await _InvoiceService.GetExportInvoice(request);
            return Ok(invoices);
        }
        [HttpGet("details/{invoiceId}")]
        public async Task<IActionResult> GetExportInvoiceById(long invoiceId)
        {
            var invoice = await _InvoiceService.GetExportInvoiceByID(invoiceId);
            if (invoice == null)
                return BadRequest("Cannot find invoice");
            return Ok(invoice);
        }
    }
}
