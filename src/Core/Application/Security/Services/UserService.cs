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

            var permissionList = dbUser.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.PermissionName)
                .Distinct()
                .ToList();

            if (dbUser == null) throw new Exception("No se encontro el usuario.");
            // Mapeamos los datos de la base de datos al DTO
            var dto = new UserDto
            {
                Id = dbUser.UserId,  // Cambio aquí para usar Guid
                Username = dbUser.Username,
                Email = dbUser.Email,
                Roles = roles,
                Avatar = dbUser.Avatar ?? "default-avatar.png",  // Si no tiene avatar, ponemos un valor por defecto
                Name = dbUser.Name ?? dbUser.Username,  // Si no tiene nombre, usamos el username
                Permissions = permissionList
            };


            return dto;
        }

        public async Task<IEnumerable<UserListDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);

            return users.Select(u => new UserListDto
            {
                UserId = u.UserId,
                EntitiId = u.EntitiId,
                Username = u.Username,
                Name = u.Name ?? u.Username,
                Email = u.Email,
                Avatar = u.Avatar,
                IsActive = u.IsActive
            });
        }
        public async Task<UserListDto> CreateAsync(
            CreateUserRequest request,
            CancellationToken cancellationToken = default)
        {
            var entity = new User
            {
                UserId = Guid.NewGuid(),
                EntitiId = request.EntitiId,
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                Avatar = request.Avatar,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(entity, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return new UserListDto
            {
                UserId = entity.UserId,
                EntitiId = entity.EntitiId,
                Username = entity.Username,
                Name = entity.Name ?? entity.Username,
                Email = entity.Email,
                Avatar = entity.Avatar,
                IsActive = entity.IsActive
            };
        }
    }
}

