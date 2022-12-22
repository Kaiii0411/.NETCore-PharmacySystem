using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly IStoreService _StoreService;
        public StoreController(IStoreService storeService)
        {
            _StoreService = storeService;
        }
        [HttpPost("create")]
        public async Task<RequestResponse> Create(StoreCreateRequest request)
        {
            var StoreId = await _StoreService.Create(request);
            if (StoreId == 0)
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
        [HttpPut("update")]
        public async Task<RequestResponse> Update(StoreUpdateRequest request)
        {
            var StoreId = await _StoreService.Update(request);
            if (StoreId == 0)
            {
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Message = "Update Failed!"
                };
            }
            return new RequestResponse
            {
                StatusCode = Code.Success,
                Message = "Update sucess!"
            };
        }
        [HttpDelete("delete/{storeId}")]
        public async Task<RequestResponse> Delete(long storeId)
        {
            var store = await _StoreService.Delete(storeId);
            if (store == 0)
            {
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Message = $"Delete Failed! Cannot find a store: {storeId}"
                };
            }
            return new RequestResponse
            {
                StatusCode = Code.Success,
                Message = "Delete sucess!"
            };
        }
        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetManageStorePagingRequest request)
        {
            var stores = await _StoreService.Get(request);
            return Ok(stores);
        }
        [HttpGet("details/{storeId}")]
        public async Task<IActionResult> GetById(int storeId)
        {
            var store = await _StoreService.GetByID(storeId);
            if (store == null)
                return BadRequest("Cannot find store");
            return Ok(store);
        }
        [HttpGet("list")]
        public async Task<RequestResponse> GetList()
        {
            try
            {
                IEnumerable<Store> stores = await _StoreService.GetListStore();
                if (stores != null && stores.Any())
                {
                    return new RequestResponse
                    {
                        StatusCode = Code.Success,
                        Content = JsonConvert.SerializeObject(stores)
                    };
                }
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Content = string.Empty
                };
            }
            catch (Exception ex)
            {
                string errorDetail = ex.InnerException != null ? ex.InnerException.ToString() : ex.Message.ToString();
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Content = errorDetail
                };
            }
        }
    }
}
