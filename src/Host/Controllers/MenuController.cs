using Microsoft.AspNetCore.Mvc;
using System.Linq;
using InventarioBackend.src.Core.Application.Menu.Interfaces;
using InventarioBackend.src.Core.Application.Menu.DTOs;

namespace InventarioBackend.src.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public ActionResult<List<MenuDto>> GetMenu()
        {
            var userRoles = User.Claims
                .Where(c => c.Type == "role")
                .Select(c => c.Value)
                .ToArray();

            var menu = _menuService.GetMenuForUser(userRoles);
            return Ok(menu);
        }
    }
}
