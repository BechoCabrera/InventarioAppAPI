using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using System.Security.Claims;

namespace InventarioBackend.src.Host.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth/logout")] // ✅ corregido para coincidir con Angular
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
            // Obtener el userId desde el token (claim 'sub' o el que uses)
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                await _authenticationService.LogoutAsync(userId);
                return Ok(new { message = "Sesión cerrada correctamente." });
            }

            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {User.Claims}, Value: {claim.Value}");
            }


            return BadRequest(new { message = "Usuario no válido." });
        }
    }
}
