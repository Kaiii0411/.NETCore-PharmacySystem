﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("create")]
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
        [HttpPut("update")]
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
        [HttpDelete("delete/{medicineGroupId}")]
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
        [HttpGet("list")]
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
        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetManageKeywordPagingRequest request)
        {
            var medicineGroups = await _MedicineGroupService.Get(request);
            return Ok(medicineGroups);
        }
        [HttpGet("details/{medicineGroupId}")]
        public async Task<IActionResult> GetById(int medicineGroupId)
        {
            var medicineGroup = await _MedicineGroupService.GetByID(medicineGroupId);
            if (medicineGroup == null)
                return BadRequest("Cannot find medicine group");
            return Ok(medicineGroup);
        }
    }
}
