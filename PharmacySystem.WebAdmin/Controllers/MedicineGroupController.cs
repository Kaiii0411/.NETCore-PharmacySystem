using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;

namespace PharmacySystem.WebAdmin.Controllers
{
    [Authorize(Roles = "Admin,StoreOwner,Staff")]
    public class MedicineGroupController : BaseController
    {
        private readonly IMedicineGroupApiClient _medicineGroupApiClient;
        private readonly IConfiguration _configuration;
        public MedicineGroupController(IConfiguration configuration, IMedicineGroupApiClient medicineGroupApiClient)
        {
            this._configuration = configuration;
            this._medicineGroupApiClient = medicineGroupApiClient;
        }
        public async Task<IActionResult> Index(string? Keyword)
        {
            var request = new GetManageKeywordPagingRequest()
            {
                Keyword = Keyword
            };
            var data = await _medicineGroupApiClient.Get(request);
            ViewBag.Keyword = Keyword;
            return View(data);
        }
        [HttpPost]
        public async Task<JsonResult> Create(MedicineGroupCreateRequest CreateMedicineGroupForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _medicineGroupApiClient.CreateMedicineGroup(CreateMedicineGroupForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var medicineGroup = await _medicineGroupApiClient.GetById(id);
            var details = new MedicineGroupUpdateRequest()
            {
                IdMedicineGroup = medicineGroup.IdMedicineGroup,
                MedicineGroupName = medicineGroup.MedicineGroupName,
                Note = medicineGroup.Note
            };
            return View(details);
        }
        [HttpPut]
        public async Task<JsonResult> Edit([FromForm] MedicineGroupUpdateRequest UpdateMedicineGroupForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _medicineGroupApiClient.UpdateMedicineGroup(UpdateMedicineGroupForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var medicineGroup = await _medicineGroupApiClient.GetById(id);
            return View(new MedicineGroupDeleteRequest()
            {
                IdMedicineGroup = medicineGroup.IdMedicineGroup,
                MedicineGroupName = medicineGroup.MedicineGroupName
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(MedicineGroupDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _medicineGroupApiClient.DeleteMedicineGroup(request.IdMedicineGroup);

            if (result)
            {
                return Json(new { result = "Redirect", url = Url.Action("Index", "MedicineGroup") });
            }
            return View(request);
        }
    }
}
