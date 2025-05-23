using InventarioBackend.src.Core.Application.Menu.DTOs;

namespace InventarioBackend.src.Core.Application.Menu.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuDto>> GetMenuForUserAsync(Guid userRoles);
        List<MenuDto> GetMenuForUser(string[] userRoles);
    }

}
