using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace PharmacySystem.WebAdmin.Controllers
{
    [Authorize(Roles="Admin,StoreOwner,Staff")]
    public class MedicineController : BaseController
    {
        private readonly IMedicineApiClient _medicineApiClient;
        private readonly IMedicineGroupApiClient _medicineGroupApiClient;
        private readonly ISupplierApiClient _supplierApiClient;
        private readonly IConfiguration _configuration;

        public MedicineController(IMedicineApiClient medicineApiClient
            , IConfiguration configuration
            , IMedicineGroupApiClient medicineGroupApiClient
            , ISupplierApiClient supplierApiClient)
        {
            this._configuration = configuration;
            this._medicineApiClient = medicineApiClient;
            this._medicineGroupApiClient = medicineGroupApiClient;
            this._supplierApiClient = supplierApiClient;
        }
        public async Task<IActionResult> Index(string? Keyword, long? IdMedicineGroup, long? IdSupplier )
        {
            var request = new GetManageMedicinePagingRequest()
            {
                Keyword = Keyword,
                IdMedicineGroup = IdMedicineGroup,
                IdSupplier = IdSupplier
            };
            var data = await _medicineApiClient.Get(request);
            ViewBag.Keyword = Keyword;

            var medicineGroupList = await _medicineGroupApiClient.GetListMedicineGroup();
            ViewBag.ListOfMedicineGroup = medicineGroupList.Select(x=> new SelectListItem()
            {
                Text = x.MedicineGroupName,
                Value = x.IdMedicineGroup.ToString(),
                Selected = IdMedicineGroup.HasValue && IdMedicineGroup.Value == x.IdMedicineGroup
            });
            var supplierList = await _supplierApiClient.GetListSupplier();
            ViewBag.ListOfSupplier = supplierList.Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.IdSupplier.ToString(),
                Selected = IdSupplier.HasValue && IdSupplier.Value == x.IdSupplier
            });

            return View(data);          
        }
        public async Task<IActionResult> Create()
        {
            var medicineGroupList = await _medicineGroupApiClient.GetListMedicineGroup();
            var supplierList = await _supplierApiClient.GetListSupplier();
            ViewBag.ListOfMedicineGroup = medicineGroupList;
            ViewBag.ListOfSupplier = supplierList;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(MedicineCreateRequest CreateMedicineForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _medicineApiClient.CreateMedicine(CreateMedicineForm);
                if (result == 0 )
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var medicine = await _medicineApiClient.GetById(id);
            var details = new MedicineUpdateRequest()
            {
                IdMedicine = medicine.IdMedicine,
                MedicineName = medicine.MedicineName,
                Description = medicine.Description,
                IdMedicineGroup = medicine.IdMedicineGroup,
                ExpiryDate = medicine.ExpiryDate,
                Quantity = medicine.Quantity,
                Unit = medicine.Unit,
                SellPrice = medicine.SellPrice,
                ImportPrice = medicine.ImportPrice,
                IdSupplier = medicine.IdSupplier
            };

            var medicineGroupList = await _medicineGroupApiClient.GetListMedicineGroup();
            ViewBag.ListOfMedicineGroup = medicineGroupList.Select(x=> new SelectListItem()
            {
                Text = x.MedicineGroupName,
                Value = x.IdMedicineGroup.ToString(),
                Selected = medicine.IdMedicineGroup == x.IdMedicineGroup
            });
            var supplierList = await _supplierApiClient.GetListSupplier();
            ViewBag.ListOfSupplier = supplierList.Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.IdSupplier.ToString(),
                Selected = medicine.IdSupplier == x.IdSupplier
            });
            return View(details);
        }
        [HttpPut]
        public async Task<JsonResult> Edit([FromForm] MedicineUpdateRequest UpdateMedicineForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _medicineApiClient.UpdateMedicine(UpdateMedicineForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var medicine = await _medicineApiClient.GetById(id);
            var medicineGroupList = await _medicineGroupApiClient.GetListMedicineGroup();
            ViewBag.ListOfMedicineGroup = medicineGroupList.Select(x => new SelectListItem()
            {
                Text = x.MedicineGroupName,
                Value = x.IdMedicineGroup.ToString(),
                Selected = medicine.IdMedicineGroup == x.IdMedicineGroup
            });
            var supplierList = await _supplierApiClient.GetListSupplier();
            ViewBag.ListOfSupplier = supplierList.Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.IdSupplier.ToString(),
                Selected = medicine.IdSupplier == x.IdSupplier
            });
            return View( new MedicineDeleteRequest() 
            {
                IdMedicine = medicine.IdMedicine,
                MedicineName = medicine.MedicineName,
                IdMedicineGroup = medicine.IdMedicineGroup,
                Unit = medicine.Unit,
                SellPrice = medicine.SellPrice,
                ImportPrice = medicine.ImportPrice,
                IdSupplier = medicine.IdSupplier
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(MedicineDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _medicineApiClient.DeleteMedicine(request.IdMedicine);

            if (result)
            {
                return Json(new { result = "Redirect", url = Url.Action("Index", "Medicine") });
            }
            return View(request);
        }
        [HttpGet]
        public async Task<List<Medicine>> GetList()
        {
            var data = await _medicineApiClient.GetListMedicine();
            return data;
        }
        public IActionResult Import()
        {
            return PartialView("_Import");
        }
        public async Task<JsonResult> ImportExcel(IFormFile file)
        {
            if (await _medicineApiClient.ImportExcel(file))
                return Json(1);
            return Json(0);
        }
        public async Task<IActionResult> OutOfDate(string? Keyword, long? IdMedicineGroup, long? IdSupplier)
        {
            var request = new GetManageMedicinePagingRequest()
            {
                Keyword = Keyword,
                IdMedicineGroup = IdMedicineGroup,
                IdSupplier = IdSupplier
            };
            var data = await _medicineApiClient.GetListOutOfDate(request);
            ViewBag.Keyword = Keyword;

            var medicineGroupList = await _medicineGroupApiClient.GetListMedicineGroup();
            ViewBag.ListOfMedicineGroup = medicineGroupList.Select(x => new SelectListItem()
            {
                Text = x.MedicineGroupName,
                Value = x.IdMedicineGroup.ToString(),
                Selected = IdMedicineGroup.HasValue && IdMedicineGroup.Value == x.IdMedicineGroup
            });
            var supplierList = await _supplierApiClient.GetListSupplier();
            ViewBag.ListOfSupplier = supplierList.Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.IdSupplier.ToString(),
                Selected = IdSupplier.HasValue && IdSupplier.Value == x.IdSupplier
            });

            return View(data);
        }
        public async Task<IActionResult> OutOfStock(string? Keyword, long? IdMedicineGroup, long? IdSupplier)
        {
            var request = new GetManageMedicinePagingRequest()
            {
                Keyword = Keyword,
                IdMedicineGroup = IdMedicineGroup,
                IdSupplier = IdSupplier
            };
            var data = await _medicineApiClient.GetListOutOfStock(request);
            ViewBag.Keyword = Keyword;

            var medicineGroupList = await _medicineGroupApiClient.GetListMedicineGroup();
            ViewBag.ListOfMedicineGroup = medicineGroupList.Select(x => new SelectListItem()
            {
                Text = x.MedicineGroupName,
                Value = x.IdMedicineGroup.ToString(),
                Selected = IdMedicineGroup.HasValue && IdMedicineGroup.Value == x.IdMedicineGroup
            });
            var supplierList = await _supplierApiClient.GetListSupplier();
            ViewBag.ListOfSupplier = supplierList.Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.IdSupplier.ToString(),
                Selected = IdSupplier.HasValue && IdSupplier.Value == x.IdSupplier
            });

            return View(data);
        }
    }
}
