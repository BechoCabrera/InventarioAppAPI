using Microsoft.AspNetCore.Mvc;
using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InventarioBackend.src.Api.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get(CancellationToken cancellationToken)
        {
            var result = await _roleService.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Post([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _roleService.CreateAsync(request, cancellationToken);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDto>> Put(Guid id, [FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _roleService.UpdateAsync(id, request, cancellationToken);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _roleService.DeleteAsync(id, cancellationToken);
            return Ok(new { message = "Rol eliminado correctamente." });
        }
    }
}
