using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using PharmacySystem.APIIntergration;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using System.Security.Cryptography.X509Certificates;

namespace PharmacySystem.WebAdmin.Controllers
{
    public class StoreController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IStoreApiClient _storeApiClient;
        public StoreController(IConfiguration configuration, IStoreApiClient storeApiClient)
        {
            this._configuration = configuration;
            this._storeApiClient = storeApiClient;
        }
        public async Task<IActionResult> Index(string? StoreName, long? IdStore)
        {
            var request = new GetManageStorePagingRequest()
            {
                StoreName = StoreName,
                IdStore = IdStore
            };
            var data = await _storeApiClient.Get(request);
            ViewBag.IdStore = IdStore;
            ViewBag.StoreName = StoreName;
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(StoreCreateRequest CreateStoreForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _storeApiClient.CreateStore(CreateStoreForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var store = await _storeApiClient.GetById(id);
            var details = new StoreUpdateRequest()
            {
                IdStore = id,
                StoreName = store.StoreName,
                Address = store.Address,
                StoreOwner = store.StoreOwner,
                Phone = store.Phone
            };
            return View(details);
        }
        [HttpPut]
        public async Task<JsonResult> Edit([FromForm] StoreUpdateRequest UpdateStoreForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _storeApiClient.UpdateStore(UpdateStoreForm);
                if (result == 0)
                {
                    return Json(0);
                }
            }
            return Json(1);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var store = await _storeApiClient.GetById(id);
            return View(new StoreDeleteRequest()
            {
                IdStore = id,
                StoreName = store.StoreName,
                Address = store.Address,
                StoreOwner = store.StoreOwner,
                Phone = store.Phone
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(StoreDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _storeApiClient.DeleteStore(request.IdStore);

            if (result)
            {
                return Json(new { result = "Redirect", url = Url.Action("Index", "Store") });
            }
            return View(request);
        }
    }
}
