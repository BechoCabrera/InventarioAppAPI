using Microsoft.AspNetCore.Mvc;
using InventarioBackend.src.Core.Application.Menu.Interfaces;
using InventarioBackend.src.Core.Application.Menu.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InventarioBackend.src.Host.Controllers
{
    [ApiController]
    [Route("api/menu")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MenuDto>>> GetMenu()
        {
            try
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(userIdStr, out var userId))
                    return Unauthorized(new { message = "Usuario no autorizado." });

                var menu = await _menuService.GetMenuForUserAsync(userId);
                if (menu == null || !menu.Any())
                    return NotFound(new { message = "No se encontró el menú para este usuario." });

                return Ok(menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Hubo un error al obtener el menú", error = ex.Message });
            }
        }


    }
}
