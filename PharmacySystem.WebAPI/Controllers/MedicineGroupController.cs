using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/medicinegroup")]
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
        public async Task<RequestResponse> Update(MedicineGroupUpdateRequest request)
        {
            var MedicineGroupId = await _MedicineGroupService.Update(request);
            if (MedicineGroupId == 0)
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
        [HttpDelete("{medicineGroupId}")]
        public async Task<RequestResponse> Delete(long medicineGroupId)
        {
            var medicine = await _MedicineGroupService.Delete(medicineGroupId);
            if (medicine == 0)
            {
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Message = $"Delete Failed! Can notfind a medicine group: {medicineGroupId}"
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
                IEnumerable<MedicineGroup> medicineGroup = await _MedicineGroupService.GetListMedicineGroup();
                if (medicineGroup != null && medicineGroup.Any())
                {
                    return new RequestResponse
                    {
                        StatusCode = Code.Success,
                        Content = JsonConvert.SerializeObject(medicineGroup)
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
