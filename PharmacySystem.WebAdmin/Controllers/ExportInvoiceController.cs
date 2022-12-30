using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PharmacySystem.APIIntergration;
using PharmacySystem.APIIntergration.Utilities;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class ExportInvoiceController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IInvoiceApiClient _invoiceApiClient;
        private readonly ISupplierApiClient _supplierApiClient;
        private readonly IMedicineApiClient _medicineApiClient;

        public ExportInvoiceController(IConfiguration configuration, IInvoiceApiClient invoiceApiClient, ISupplierApiClient supplierApiClient, IMedicineApiClient medicineApiClient)
        {
            this._configuration = configuration;
            this._invoiceApiClient = invoiceApiClient;
            this._supplierApiClient = supplierApiClient;
            this._medicineApiClient = medicineApiClient;
        }
        public async Task<IActionResult> Index(DateTime? DateCheckIn, DateTime? DateCheckOut, long? IdSupplier, int? StatusID)
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
        public async Task<IActionResult> Create()
        {
            var medicineList = await _medicineApiClient.GetListMedicine();
            ViewBag.ListOfMedicine = medicineList.Select(x => new SelectListItem()
            {
                Text = x.MedicineName,
                Value = x.IdMedicine.ToString(),
            });
            return View(GetEInvoiceViewModel());
        }
        public async Task<IActionResult> CreateConfirm(ExportInvoiceCreateRequest CreateEInvoiceForm)
        {
            var model = GetEInvoiceViewModel();
            var invoiceDetails = new List<InvoiceDetailsVM>();
            foreach (var item in model.EInvoiceItems)
            {
                invoiceDetails.Add(new InvoiceDetailsVM()
                {
                    MedicineId = item.IIdMedicine,
                    MedicineName = item.IMedicineName,
                    Quantity = item.IQuantity,
                    TotalPrice = item.IQuantity * item.IPrice
                });
            }
            var createRequest = new ExportInvoiceCreateRequest()
            {
                IdStaff = 1,
                DateCheckIn = DateTime.Now,
                DateCheckOut = DateTime.Now,
                StatusID = 6,
                Note = CreateEInvoiceForm.Note,
                InvoiceDetails = invoiceDetails
            };
            await _invoiceApiClient.CreateExportInvoice(createRequest);
            return RedirectToAction("Index", "ExportInvoice");
        }
        public async Task<IActionResult> Details(long id)
        {
            var details = await _invoiceApiClient.GetExportInvoiceByID(id);
            return View(details);
        }
        public async Task<IActionResult> AddToInvoice(long id, int quantity)
        {
            var medicine = await _medicineApiClient.GetById(id);
            var session = HttpContext.Session.GetString(SystemConstants.EInvoice);
            List<EInvoice> currentInvoice = new List<EInvoice>();
            if (session != null)
                currentInvoice = JsonConvert.DeserializeObject<List<EInvoice>>(session);

            var existItem = currentInvoice.FirstOrDefault(x => x.IIdMedicine == id);
            if (existItem != null)
            {
                quantity = quantity != 0 ? quantity : 1;
                existItem.IQuantity = existItem.IQuantity + quantity;
            }
            else
            {
                quantity = quantity != 0 ? quantity : 1;
                var invoiceItems = new EInvoice()
                {
                    IIdMedicine = id,
                    IMedicineName = medicine.MedicineName,
                    IQuantity = quantity,
                    IPrice = medicine.ImportPrice
                };

                currentInvoice.Add(invoiceItems);
            }
            HttpContext.Session.SetString(SystemConstants.EInvoice, JsonConvert.SerializeObject(currentInvoice));
            return Ok(currentInvoice);
        }
        public IActionResult UpdateInvoice(int id, int quantity)
        {
            var session = HttpContext.Session.GetString(SystemConstants.EInvoice);
            List<EInvoice> currentInvoice = new List<EInvoice>();
            if (session != null)
                currentInvoice = JsonConvert.DeserializeObject<List<EInvoice>>(session);

            foreach (var item in currentInvoice)
            {
                if (item.IIdMedicine == id)
                {
                    if (quantity == 0)
                    {
                        currentInvoice.Remove(item);
                        break;
                    }
                    item.IQuantity = quantity;
                }
            }

            HttpContext.Session.SetString(SystemConstants.EInvoice, JsonConvert.SerializeObject(currentInvoice));
            return Ok();
        }
        public IActionResult RemoveItemsInvoice(int id)
        {
            var session = HttpContext.Session.GetString(SystemConstants.EInvoice);
            List<EInvoice> currentInvoice = new List<EInvoice>();
            if (session != null)
                currentInvoice = JsonConvert.DeserializeObject<List<EInvoice>>(session);
            foreach (var item in currentInvoice.ToList())
            {
                if (item.IIdMedicine == id)
                    currentInvoice.Remove(item);
            }

            HttpContext.Session.SetString(SystemConstants.EInvoice, JsonConvert.SerializeObject(currentInvoice));
            return Ok();
        }
        private EInvoiceVM GetEInvoiceViewModel()
        {
            var session = HttpContext.Session.GetString(SystemConstants.EInvoice);
            List<EInvoice> currentInvoice = new List<EInvoice>();
            if (session != null)
                currentInvoice = JsonConvert.DeserializeObject<List<EInvoice>>(session);
            var einvoiceVM = new EInvoiceVM()
            {
                EInvoiceItems = currentInvoice,
                CreateEInvoiceModel = new ImportInvoiceCreateRequest()
            };
            return einvoiceVM;
        }
        public IActionResult CheckOut(ExportInvoiceCreateRequest CreateEInvoiceForm)
        {
            var model = GetEInvoiceViewModel();
            var invoiceDetails = new List<InvoiceDetailsVM>();
            foreach (var item in model.EInvoiceItems)
            {
                invoiceDetails.Add(new InvoiceDetailsVM()
                {
                    MedicineId = item.IIdMedicine,
                    MedicineName = item.IMedicineName,
                    Quantity = item.IQuantity,
                    TotalPrice = item.IQuantity * item.IPrice
                });
            }
            return View(model);
        }
    }
}
