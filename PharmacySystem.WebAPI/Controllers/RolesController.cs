using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using PharmacySystem.Service;

namespace PharmacySystem.WebAPI.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IRolesService _roleService;
        public RolesController(IRolesService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }
        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] GetRolesPagingRequest request)
        {
            var roles = await _roleService.Get(request);
            return Ok(roles);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RoleUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var roles = await _roleService.GetById(id);
            return Ok(roles);
        }
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] RoleCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("users/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersByRoles(Guid id)
        {
            var roles = await _roleService.GetUsersByRoles(id);
            return Ok(roles);
        }
        [HttpPut("{id}/users")]
        public async Task<IActionResult> EditUsersInRoles(Guid id, UserAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.EditUsersInRoles(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
