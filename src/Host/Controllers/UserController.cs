using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InventarioBackend.src.Host.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUser()
        {
            var userName = User.Identity?.Name ?? "Unknown";
            var email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value ?? "no-email@example.com";

            return Ok(new { Name = userName, Email = email });
        }
    }
}
