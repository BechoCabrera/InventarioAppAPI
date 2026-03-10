using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Application.Security.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<PermissionDto> CreateAsync(
            CreatePermissionRequest request,
            CancellationToken cancellationToken = default);
    }
}
