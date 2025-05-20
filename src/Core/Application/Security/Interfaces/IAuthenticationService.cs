using System.Threading.Tasks;
using InventarioBackend.src.Core.Application.Security.DTOs;

namespace InventarioBackend.src.Core.Application.Security.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task LogoutAsync(string userId);
    }
}
