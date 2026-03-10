using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using InventarioBackend.src.Core.Domain.Security.Entities;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using InventarioBackend.src.Infrastructure.Data.Repositories.Security;

namespace InventarioBackend.src.Core.Application.Security.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var roles = await _roleRepository.GetAllAsync(cancellationToken);

            return roles.Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                Description = r.Description,
                PermissionIds = r.RolePermissions
            .Select(rp => rp.PermissionId)
            .ToList()
            });
        }

        public async Task<RoleDto> CreateAsync(
            CreateRoleRequest request,
            CancellationToken cancellationToken = default)
        {
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = request.RoleName,
                Description = request.Description
            };

            // Si vienen permisos iniciales, los agregamos al rol
            if (request.PermissionIds is { Count: > 0 })
            {
                foreach (var permId in request.PermissionIds.Distinct())
                {
                    role.RolePermissions.Add(new RolePermission
                    {
                        RoleId = role.RoleId,
                        PermissionId = permId
                    });
                }
            }

            await _roleRepository.AddAsync(role, cancellationToken);
            await _roleRepository.SaveChangesAsync(cancellationToken);

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description,
                PermissionIds = request.PermissionIds?.ToList()
            };
        }
        public async Task<RoleDto> UpdateAsync(Guid id, CreateRoleRequest request, CancellationToken cancellationToken = default)
        {
            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
            if (role == null)
                throw new Exception("Rol no encontrado.");

            role.RoleName = request.RoleName;
            role.Description = request.Description;

            // Actualizar permisos si vienen en la petición
            if (request.PermissionIds != null)
            {
                // Limpiar permisos actuales
                role.RolePermissions.Clear();
                foreach (var permId in request.PermissionIds.Distinct())
                {
                    role.RolePermissions.Add(new RolePermission
                    {
                        RoleId = role.RoleId,
                        PermissionId = permId
                    });
                }
            }

            await _roleRepository.UpdateAsync(role, cancellationToken);
            await _roleRepository.SaveChangesAsync(cancellationToken);

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description,
                PermissionIds = request.PermissionIds?.ToList()
            };
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
            if (role == null)
                throw new Exception("Rol no encontrado.");

            if (role.UserRoles != null && role.UserRoles.Any())
                throw new Exception("No se puede eliminar el rol porque hay usuarios asignados.");

            await _roleRepository.DeleteAsync(role, cancellationToken);
            await _roleRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
