using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [HttpGet("details/{medicineId}")]
        public async Task<IActionResult> GetById(int medicineId)
        {
            var medicine = await _MedicineService.GetByID(medicineId);
            if (medicine == null)
                return BadRequest("Cannot find medicine");
            return Ok(medicine);
        }
        [HttpGet("list")]
        public async Task<RequestResponse> GetList()
        {
            try
            {
                IEnumerable<Medicine> medicines = await _MedicineService.GetListMedicine();
                if (medicines != null && medicines.Any())
                {
                    return new RequestResponse
                    {
                        StatusCode = Code.Success,
                        Content = JsonConvert.SerializeObject(medicines)
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
        [HttpGet("listbymedicinegroupid/{IdMedicineGroup}")]
        public async Task<IActionResult> GetByIdMedicine(long IdMedicineGroup)
        {
            var list = await _MedicineService.GetListMedicineByMecicineGroup(IdMedicineGroup);
            if (list == null)
                return BadRequest("Cannot find details");
            return Ok(list);
        }
        [HttpGet("paging/outstock")]
        public async Task<IActionResult> GetListOutOfStock([FromQuery] GetManageMedicinePagingRequest request)
        {
            var medicines = await _MedicineService.GetListOutOfStock(request);
            return Ok(medicines);
        }
        [HttpGet("paging/outdate")]
        public async Task<IActionResult> GetListOutOfDate([FromQuery] GetManageMedicinePagingRequest request)
        {
            var medicines = await _MedicineService.GetListOutOfDate(request);
            return Ok(medicines);
        }
        [HttpPost("import")]
        public async Task<bool> ImportMedicinesByExcel(IFormFile file)
        {
            if(await _MedicineService.ImportMedicineByExcel(file))
            {
                return true;
            }
            return false;
        }
    }
}
