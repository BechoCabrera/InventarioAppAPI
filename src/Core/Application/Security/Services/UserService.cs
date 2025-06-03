using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using InventarioBackend.src.Core.Domain.Security.Entities;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventarioBackend.src.Core.Application.Security.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; // Inyectamos el repositorio de usuario

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var username = user.Identity?.Name ?? "Unknown";

            var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0";
            var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "no-email@example.com";
            

            var roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            User? dbUser = await _userRepository.GetByIdAsync(Guid.Parse(id));

         

            // Mapeamos los datos de la base de datos al DTO
            var dto = new UserDto
            {
                Id = dbUser.UserId,  // Cambio aquí para usar Guid
                Username = dbUser.Username,
                Email = dbUser.Email,
                Roles = roles,
                Avatar = dbUser.Avatar ?? "default-avatar.png",  // Si no tiene avatar, ponemos un valor por defecto
                Name = dbUser.Name ?? dbUser.Username  // Si no tiene nombre, usamos el username
            };


            return dto;
        }
    }
}
