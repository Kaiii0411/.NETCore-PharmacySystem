using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _SupplierService;
        public SupplierController(ISupplierService SupplierService)
        {
            this._SupplierService = SupplierService;
        }
        [HttpPost]
        public async Task<RequestResponse> Create(SupplierCreateRequest request)
        {
            var SupplierId = await _SupplierService.Create(request);
            if (SupplierId == 0)
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
        [HttpPut]
        public async Task<RequestResponse> Update(SupplierUpdateRequest request)
        {
            var SupplierId = await _SupplierService.Update(request);
            if (SupplierId == 0)
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
        [HttpDelete("{supplierId}")]
        public async Task<RequestResponse> Delete(long supplierId)
        {
            var supplier = await _SupplierService.Delete(supplierId);
            if (supplier == 0)
            {
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Message = $"Delete Failed! Can notfind a supplier: {supplierId}"
                };
            }
            return new RequestResponse
            {
                StatusCode = Code.Success,
                Message = "Delete sucess!"
            };
        }
        [HttpGet]
        public async Task<RequestResponse> GetList()
        {
            try
            {
                IEnumerable<Supplier> supplier = await _SupplierService.GetListSupplier();
                if (supplier != null && supplier.Any())
                {
                    return new RequestResponse
                    {
                        StatusCode = Code.Success,
                        Content = JsonConvert.SerializeObject(supplier)
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
