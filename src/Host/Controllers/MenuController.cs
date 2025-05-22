using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventarioBackend.src.Core.Application.Menu.Interfaces;
using InventarioBackend.src.Core.Application.Menu.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace InventarioBackend.src.Host.Controllers
{
    [ApiController]
    [Route("api/menu")]
    [Authorize] // Esto asegura que el acceso al menú solo lo puedan hacer los usuarios autenticados
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost]
        public ActionResult<List<MenuDto>> GetMenu()
        {
            Console.WriteLine("✅ Entró al controlador MenuController (POST)");
            Console.WriteLine("🔐 Authorization Header: " + Request.Headers["Authorization"]);

            try
            {
                var userRoles = User.Claims
                    .Where(c => c.Type == "role")
                    .Select(c => c.Value)
                    .ToArray();

                var menu = _menuService.GetMenuForUser(userRoles);

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
