using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventarioBackend.src.Host.Controllers
{
    [ApiController]
    [Route("api/user-roles")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        // GET /api/user-roles/user/{userId}
        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetByUser(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var result = await _userRoleService.GetByUserAsync(userId, cancellationToken);
            return Ok(result);
        }

        // PUT /api/user-roles/user/{userId}
        [HttpPut("user/{userId:guid}")]
        public async Task<IActionResult> UpdateUserRoles(
            Guid userId,
            [FromBody] UpdateUserRolesRequest request,
            CancellationToken cancellationToken)
        {
            if (userId == Guid.Empty)
                return BadRequest("El userId de la ruta no puede ser un GUID vacío.");

            request.UserId = userId;
            if (request.RoleIds == null) request.RoleIds = new List<Guid>();

            await _userRoleService.UpdateUserRolesAsync(request, cancellationToken);
            return Ok();
        }
    }
}
