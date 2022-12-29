using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using PharmacySystem.APIIntergration;
using System.Text;
using PharmacySystem.Models.Common;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class MainInvoiceController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IInvoiceApiClient _invoiceApiClient;
        private readonly ISupplierApiClient _supplierApiClient;
        private readonly IMedicineApiClient _medicineApiClient;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public MainInvoiceController(IConfiguration configuration, IInvoiceApiClient invoiceApiClient, ISupplierApiClient supplierApiClient, IMedicineApiClient medicineApiClient, IWebHostEnvironment webHostEnviroment)
        {
            this._configuration = configuration;
            this._invoiceApiClient = invoiceApiClient;
            this._supplierApiClient = supplierApiClient;
            this._medicineApiClient = medicineApiClient;
            this._webHostEnviroment = webHostEnviroment;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        public async Task<IActionResult> IInvoiceIndex(DateTime? DateCheckIn, DateTime? DateCheckOut, long? IdSupplier, int? StatusID)
        {
            var request = new GetManageIInvoicePagingRequest()
            {
                DateCheckIn = DateCheckIn,
                DateCheckOut = DateCheckOut,
                IdSupplier = IdSupplier,
                StatusID = StatusID
            };
            var data = await _invoiceApiClient.GetImportInvoice(request);
            ViewBag.DateCheckIn = DateCheckIn;
            ViewBag.DateCheckOut = DateCheckOut;

            var supplierList = await _supplierApiClient.GetListSupplier();
            ViewBag.ListOfSupplier = supplierList.Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.IdSupplier.ToString(),
                Selected = IdSupplier.HasValue && IdSupplier.Value == x.IdSupplier
            });
            return View(data);
        }
        public async Task<IActionResult> EInvoiceIndex(DateTime? DateCheckIn, DateTime? DateCheckOut, long? IdSupplier, int? StatusID)
        {
            var request = new GetManageEInvoicePagingRequest()
            {
                DateCheckIn = DateCheckIn,
                DateCheckOut = DateCheckOut,
                StatusID = StatusID
            };
            var data = await _invoiceApiClient.GetExportInvoice(request);
            ViewBag.DateCheckIn = DateCheckIn;
            ViewBag.DateCheckOut = DateCheckOut;
            return View(data);
        }
        public async Task<IActionResult> IInvoiceDetails(long id)
        {
            var details = await _invoiceApiClient.GetImportInvoiceByID(id);

            var supplierList = await _supplierApiClient.GetListSupplier();
            ViewBag.ListOfSupplier = supplierList.Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.IdSupplier.ToString(),
                Selected = details.IdSupllier.HasValue && details.IdSupllier.Value == x.IdSupplier
            });
            return View(details);
        }
        public async Task<IActionResult> EInvoiceDetails(long id)
        {
            var details = await _invoiceApiClient.GetExportInvoiceByID(id);
            return View(details);
        }
        [HttpPut]
        public async Task<JsonResult> UpdateProcess([FromForm] ProcessRequest ProcessForm)
        {
            int StatusID = ConvertStatus(ProcessForm.Status);
            if (ModelState.IsValid && StatusID != 4)
            {
                var result = await _invoiceApiClient.ProcessApproved(ProcessForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        private int ConvertStatus(string StatusName)
        {
            int StatusID = 8;
            switch (StatusName)
            {
                case "New":
                    StatusID = (int)EStatus.NEW;
                    break;
                case "Approved":
                    StatusID = (int)EStatus.APPROVED;
                    break;
                case "Rejected":
                    StatusID = (int)EStatus.REJECTED;
                    break;
                case "Done":
                    StatusID = (int)EStatus.DONE;
                    break;
                case "Received":
                    StatusID = (int)EStatus.RECEIVED;
                    break;
                case "Error":
                    StatusID = (int)EStatus.ERROR;
                    break;
            }
            return StatusID;
        }
    }
}
