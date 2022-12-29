using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacySystem.Models.ViewModels;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using PharmacySystem.Models.Common;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly ISupplierApiClient _supplierApiClient;
        private readonly ISupplierGroupApiClient _supplierGroupApiClient;
        private readonly IConfiguration _configuration;
        private readonly IInvoiceApiClient _invoiceApiClient;

        public SupplierController(ISupplierApiClient supplierApiClient
            , IConfiguration configuration
            , ISupplierGroupApiClient supplierGroupApiClient
            , IInvoiceApiClient invoiceApiClient)
        {
            this._supplierApiClient = supplierApiClient;
            this._configuration = configuration;
            this._supplierGroupApiClient = supplierGroupApiClient;
            this._invoiceApiClient = invoiceApiClient;
        }
        //Index
        public async Task<IActionResult> Index(string? Keyword, long? IdSupplierGroup)
        {
            var request = new GetManageSupplierPagingRequest()
            {
                Keyword = Keyword,
                IdSupplierGroup = IdSupplierGroup
            };
            var data = await _supplierApiClient.Get(request);
            ViewBag.Keyword = Keyword;

            var supplierGroupList = await _supplierGroupApiClient.GetListSupplierGroup();
            ViewBag.ListOfSupplierGroup = supplierGroupList.Select(x => new SelectListItem()
            {
                Text = x.SupplierGroupName,
                Value = x.IdSupplierGroup.ToString(),
                Selected = IdSupplierGroup.HasValue && IdSupplierGroup.Value == x.IdSupplierGroup
            });
            return View(data);
        }
        //Create
        public async Task<IActionResult> Create()
        {
            var supplierGroupList = await _supplierGroupApiClient.GetListSupplierGroup();
            ViewBag.ListOfSupplierGroup = supplierGroupList;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(SupplierCreateRequest CreateSupplierForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _supplierApiClient.CreateSupplier(CreateSupplierForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var supplier = await _supplierApiClient.GetById(id);
            var details = new SupplierUpdateRequest()
            {
                IdSupplier = supplier.IdSupplier,
                SupplierName = supplier.SupplierName,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                IdSupplierGroup = supplier.IdSupplierGroup
            };
            var supplierGroupList = await _supplierGroupApiClient.GetListSupplierGroup();
            ViewBag.ListOfSupplierGroup = supplierGroupList.Select(x => new SelectListItem()
            {
                Text = x.SupplierGroupName,
                Value = x.IdSupplierGroup.ToString(),
                Selected = supplier.IdSupplierGroup == x.IdSupplierGroup
            });
            return View(details);
        }
        [HttpPut]
        public async Task<JsonResult> Edit([FromForm] SupplierUpdateRequest UpdateSupplierForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _supplierApiClient.UpdateSupplier(UpdateSupplierForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var supplier = await _supplierApiClient.GetById(id);
            var supplierGroupList = await _supplierGroupApiClient.GetListSupplierGroup();
            ViewBag.ListOfSupplierGroup = supplierGroupList.Select(x => new SelectListItem()
            {
                Text = x.SupplierGroupName,
                Value = x.IdSupplierGroup.ToString(),
                Selected = supplier.IdSupplierGroup == x.IdSupplierGroup
            });
            return View(new SupplierDeleteRequest()
            {
                IdSupplier = supplier.IdSupplier,
                SupplierName = supplier.SupplierName,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                IdSupplierGroup = supplier.IdSupplierGroup
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(SupplierDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _supplierApiClient.DeleteSupplier(request.IdSupplier);

            if (result)
            {
                return Json(new { result = "Redirect", url = Url.Action("Index", "Supplier") });
            }
            return View(request);
        }
        public async Task<IActionResult> InvoiceIndex(DateTime? DateCheckIn, DateTime? DateCheckOut, long? IdSupplier, int? StatusID)
        {
            var request = new GetManageIInvoicePagingRequest()
            {
                DateCheckIn = DateCheckIn,
                DateCheckOut = DateCheckOut,
                IdSupplier = IdSupplier,
                StatusID = StatusID
            };
            var data = await _invoiceApiClient.GetImportInvoiceForSupplier(request);
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
        [HttpPut]
        public async Task<JsonResult> UpdateProcess([FromForm] ProcessRequest ProcessForm)
        {
            int StatusID = ConvertStatus(ProcessForm.Status);
            if (ModelState.IsValid && StatusID == 4)
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
