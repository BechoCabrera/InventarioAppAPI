using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventarioBackend.src.Core.Application.Security.Interfaces;

namespace InventarioBackend.Host.Controllers
{
    [ApiController]
    [Route("api/logout")]
    public class LogoutController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LogoutController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Suponiendo que puedes obtener userId desde el token o contexto
            var userId = User.FindFirst("sub")?.Value;
            if (userId != null)
            {
                await _authenticationService.LogoutAsync(userId);
                return Ok(new { message = "Sesión cerrada correctamente." });
            }
            return BadRequest(new { message = "Usuario no válido." });
        }
    }
}
