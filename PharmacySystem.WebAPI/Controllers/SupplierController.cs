using Microsoft.AspNetCore.Mvc;
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
                    Status = Code.Failed,
                    Message = "Add Failed!"
                };
            }
            return new RequestResponse
            {
                Status = Code.Success,
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
                    Status = Code.Failed,
                    Message = "Update Failed!"
                };
            }
            return new RequestResponse
            {
                Status = Code.Success,
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
                    Status = Code.Failed,
                    Message = $"Delete Failed! Can notfind a supplier: {supplierId}"
                };
            }
            return new RequestResponse
            {
                Status = Code.Success,
                Message = "Delete sucess!"
            };
        }
    }
}
