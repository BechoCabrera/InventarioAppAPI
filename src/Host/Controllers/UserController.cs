using Microsoft.AspNetCore.Mvc;

namespace InventarioBackend.src.Host.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUser()
        {
            // Aquí devuelves datos de usuario o lo que necesites
            return Ok(new { Name = "Demo User", Email = "demo@example.com" });
        }
    }
}