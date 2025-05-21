using InventarioBackend.src.Core.Application.Menu.DTOs;

namespace InventarioBackend.src.Core.Application.Menu.Interfaces
{
    public interface IMenuService
    {
        List<MenuDto> GetMenuForUser(string[] userRoles);
    }

}
