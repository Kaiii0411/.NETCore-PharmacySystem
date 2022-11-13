using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineGroupController : Controller
    {
        private readonly IMedicineGroupService _MedicineGroupService;
        public MedicineGroupController(IMedicineGroupService MedicineGroupSerive)
        {
            this._MedicineGroupService = MedicineGroupSerive;
        }
        [HttpPost]
        public async Task<RequestResponse> Create(MedicineGroupCreateRequest request)
        {
            var MedicineGroupId = await _MedicineGroupService.Create(request);
            if (MedicineGroupId == 0)
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
        public async Task<RequestResponse> Update(MedicineGroupUpdateRequest request)
        {
            var MedicineGroupId = await _MedicineGroupService.Update(request);
            if (MedicineGroupId == 0)
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
        [HttpDelete("{medicineGroupId}")]
        public async Task<RequestResponse> Delete(long medicineGroupId)
        {
            var medicine = await _MedicineGroupService.Delete(medicineGroupId);
            if (medicine == 0)
            {
                return new RequestResponse
                {
                    Status = Code.Failed,
                    Message = $"Delete Failed! Can notfind a medicine group: {medicineGroupId}"
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
