using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierGroupController : Controller
    {
        private readonly ISupplierGroupService _SupplierGroupService;
        public SupplierGroupController(ISupplierGroupService SupplierGroupService)
        {
            this._SupplierGroupService = SupplierGroupService;
        }
        [HttpPost]
        public async Task<RequestResponse> Create(SupplierGroupCreateRequest request)
        {
            var SupplierGroupId = await _SupplierGroupService.Create(request);
            if (SupplierGroupId == 0)
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
        public async Task<RequestResponse> Update(SupplierGroupUpdateRequest request)
        {
            var SupplierGroupId = await _SupplierGroupService.Update(request);
            if (SupplierGroupId == 0)
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
        [HttpDelete("{supplierGroupId}")]
        public async Task<RequestResponse> Delete(long supplierGroupId)
        {
            var supplierGroup = await _SupplierGroupService.Delete(supplierGroupId);
            if (supplierGroup == 0)
            {
                return new RequestResponse
                {
                    Status = Code.Failed,
                    Message = $"Delete Failed! Can notfind a supplier group: {supplierGroupId}"
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
