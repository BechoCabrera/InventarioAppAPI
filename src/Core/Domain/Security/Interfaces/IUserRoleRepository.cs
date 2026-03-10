using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Domain.Security.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<List<UserRole>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(IEnumerable<UserRole> entities, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<UserRole> entities, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
