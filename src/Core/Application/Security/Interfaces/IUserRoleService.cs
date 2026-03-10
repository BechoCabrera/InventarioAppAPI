using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Application.Security.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDto>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task UpdateUserRolesAsync(UpdateUserRolesRequest request, CancellationToken cancellationToken = default);
    }
}
