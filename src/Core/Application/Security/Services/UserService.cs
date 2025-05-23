using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventarioBackend.src.Core.Application.Security.Services
{
    public class UserService : IUserService
    {
        public Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var username = user.Identity?.Name ?? "Unknown";

            var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0";
            var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "no-email@example.com";

            var roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            var dto = new UserDto
            {
                Id = int.TryParse(id, out var userId) ? userId : 0,
                Username = username,
                Email = email,
                Roles = roles
            };

            return Task.FromResult(dto);
        }
    }
}
