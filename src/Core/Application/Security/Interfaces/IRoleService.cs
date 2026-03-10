using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Application.Security.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<RoleDto> CreateAsync(CreateRoleRequest request, CancellationToken cancellationToken = default);
        Task<RoleDto> UpdateAsync(Guid id, CreateRoleRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);


    }
}
