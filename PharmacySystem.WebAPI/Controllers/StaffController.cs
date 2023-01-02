using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using PharmacySystem.Service;
using Newtonsoft.Json;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IStaffService _StaffService;
        public StaffController(IStaffService staffService)
        {
            this._StaffService = staffService;
        }
        [HttpPost("create")]
        public async Task<RequestResponse> Create(StaffCreateRequest request)
        {
            var StaffId = await _StaffService.Create(request);
            if (StaffId == 0)
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
        public async Task<RequestResponse> Update(StaffUpdateRequest request)
        {
            var StaffId = await _StaffService.Update(request);
            if (StaffId == 0)
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
        [HttpDelete("delete/{staffId}")]
        public async Task<RequestResponse> Delete(long staffId)
        {
            var staff = await _StaffService.Delete(staffId);
            if (staff == 0)
            {
                return new RequestResponse
                {
                    StatusCode = Code.Failed,
                    Message = $"Delete Failed! Cannot find a staff: {staffId}"
                };
            }
            return new RequestResponse
            {
                StatusCode = Code.Success,
                Message = "Delete sucess!"
            };
        }
        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetManageStaffPagingRequest request)
        {
            var staff = await _StaffService.Get(request);
            return Ok(staff);
        }
        [HttpGet("details/{staffId}")]
        public async Task<IActionResult> GetById(int staffId)
        {
            var staff = await _StaffService.GetByID(staffId);
            if (staff== null)
                return BadRequest("Cannot find staff");
            return Ok(staff);
        }
        [HttpGet("list")]
        public async Task<RequestResponse> GetList()
        {
            try
            {
                IEnumerable<staff> staffs = await _StaffService.GetListStaff();
                if (staffs != null && staffs.Any())
                {
                    return new RequestResponse
                    {
                        StatusCode = Code.Success,
                        Content = JsonConvert.SerializeObject(staffs)
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
        [HttpGet("newlist")]
        public async Task<RequestResponse> GetListNew()
        {
            try
            {
                IEnumerable<staff> staffs = await _StaffService.GetListNewStaff();
                if (staffs != null && staffs.Any())
                {
                    return new RequestResponse
                    {
                        StatusCode = Code.Success,
                        Content = JsonConvert.SerializeObject(staffs)
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
