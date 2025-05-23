using InventarioBackend.src.Core.Application.Security.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventarioBackend.src.Core.Application.Security.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal user);
    }
}
