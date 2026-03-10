using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using InventarioBackend.src.Core.Domain.Security.Entities;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using InventarioBackend.src.Infrastructure.Data.Repositories.Security;

namespace InventarioBackend.src.Core.Application.Security.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var list = await _permissionRepository.GetAllAsync(cancellationToken);

            return list.Select(p => new PermissionDto
            {
                PermissionId = p.PermissionId,
                PermissionName = p.PermissionName,
                Description = p.Description
            });
        }

        public async Task<PermissionDto> CreateAsync(
            CreatePermissionRequest request,
            CancellationToken cancellationToken = default)
        {
            var entity = new Permission
            {
                PermissionId = Guid.NewGuid(),
                PermissionName = request.PermissionName,
                Description = request.Description
            };

            await _permissionRepository.AddAsync(entity, cancellationToken);
            await _permissionRepository.SaveChangesAsync(cancellationToken);

            return new PermissionDto
            {
                PermissionId = entity.PermissionId,
                PermissionName = entity.PermissionName,
                Description = entity.Description
            };
        }
    }
}
