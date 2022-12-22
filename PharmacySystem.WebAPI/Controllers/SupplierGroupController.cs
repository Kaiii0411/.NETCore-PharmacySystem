using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/suppliergroup")]
    [ApiController]
    public class SupplierGroupController : Controller
    {
        private readonly ISupplierGroupService _SupplierGroupService;
        public SupplierGroupController(ISupplierGroupService SupplierGroupService)
        {
            this._SupplierGroupService = SupplierGroupService;
        }
        [HttpPost("create")]
        public async Task<RequestResponse> Create(SupplierGroupCreateRequest request)
        {
            var SupplierGroupId = await _SupplierGroupService.Create(request);
            if (SupplierGroupId == 0)
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
        public async Task<RequestResponse> Update(SupplierGroupUpdateRequest request)
        {
            var SupplierGroupId = await _SupplierGroupService.Update(request);
            if (SupplierGroupId == 0)
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
        [HttpDelete("delete/{supplierGroupId}")]
        public async Task<RequestResponse> Delete(long supplierGroupId)
        {
            var supplierGroup = await _SupplierGroupService.Delete(supplierGroupId);
            if (supplierGroup == 0)
            {
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Message = $"Delete Failed! Can notfind a supplier group: {supplierGroupId}"
                };
            }
            return new RequestResponse
            {
                StatusCode = Code.Success,
                Message = "Delete sucess!"
            };
        }
        [HttpGet("list")]
        public async Task<RequestResponse> GetList()
        {
            try
            {
                IEnumerable<SupplierGroup> supplierGroup = await _SupplierGroupService.GetListSupplierGroup();
                if (supplierGroup != null && supplierGroup.Any())
                {
                    return new RequestResponse
                    {
                        StatusCode = Code.Success,
                        Content = JsonConvert.SerializeObject(supplierGroup)
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
        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetManageKeywordPagingRequest request)
        {
            var supplierGroups = await _SupplierGroupService.Get(request);
            return Ok(supplierGroups);
        }
        [HttpGet("details/{supplierGroupId}")]
        public async Task<IActionResult> GetById(int supplierGroupId)
        {
            var supllierGroup = await _SupplierGroupService.GetByID(supplierGroupId);
            if (supllierGroup == null)
                return BadRequest("Cannot find supplier group");
            return Ok(supllierGroup);
        }
    }
}
