﻿using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/medicines")]
    [ApiController]
    public class MedicineController : Controller
    {
        private readonly IMedicineService _MedicineService;
        public MedicineController(IMedicineService MedicineSerive)
        {
            this._MedicineService = MedicineSerive;
        }
        [HttpPost("create")]
        public async Task<RequestResponse> Create(MedicineCreateRequest request)
        {
            var MedicineId = await _MedicineService.Create(request);
            if(MedicineId == 0)
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
        public async Task<RequestResponse> Update(MedicineUpdateRequest request)
        {
            var MedicineId = await _MedicineService.Update(request);
            if (MedicineId == 0)
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
        [HttpDelete("delete/{medicineId}")]
        public async Task<RequestResponse> Delete(long medicineId)
        {
            var medicine = await _MedicineService.Delete(medicineId);
            if(medicine == 0)
            {
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Message = $"Delete Failed! Can notfind a medicine: {medicineId}"
                };
            }
            return new RequestResponse
            {
                StatusCode = Code.Success,
                Message = "Delete sucess!"
            };
        }
        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetManageMedicinePagingRequest request)
        {
            var medicines = await _MedicineService.Get(request);
            return Ok(medicines);
        }
    }
}
