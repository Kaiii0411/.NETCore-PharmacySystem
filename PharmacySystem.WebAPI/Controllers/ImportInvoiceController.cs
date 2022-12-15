using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/importinvoices")]
    [ApiController]
    public class ImportInvoiceController : Controller
    {
        private readonly IInvoiceService _InvoiceService;
        private readonly PharmacySystemContext _context;
        //crud
        public ImportInvoiceController(PharmacySystemContext context, IInvoiceService InvoiceService)
        {
            this._context = context;
            this._InvoiceService = InvoiceService;
        }
        [HttpPost("create")]
        public async Task<RequestResponse> Create(ImportInvoiceCreateRequest request)
        {
            var InvoiceId = await _InvoiceService.AddImportInvoice(request);
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
            var medicine = await _InvoiceService.DeleteImportInvoice(invoiceId);
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
        public async Task<IActionResult> Get([FromQuery] GetManageIInvoicePagingRequest request)
        {
            var invoices = await _InvoiceService.GetImportInvoice(request);
            return Ok(invoices);
        }
        [HttpGet("details/{invoiceId}")]
        public async Task<IActionResult> GetImportInvoiceById(long invoiceId)
        {
            var invoice = await _InvoiceService.GetImportInvoiceByID(invoiceId);
            if (invoice == null)
                return BadRequest("Cannot find invoice");
            return Ok(invoice);
        }
    }
}
