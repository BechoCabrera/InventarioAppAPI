using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventarioBackend.src.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/permissions
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> Get(CancellationToken cancellationToken)
        {
            var result = await _permissionService.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDto>> Post(
            [FromBody] CreatePermissionRequest request,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _permissionService.CreateAsync(request, cancellationToken);
            return Ok(created);
        }
    }
}
