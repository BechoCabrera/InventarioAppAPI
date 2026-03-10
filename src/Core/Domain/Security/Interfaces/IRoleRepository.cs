using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Domain.Security.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Role entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Role entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Role entity, CancellationToken cancellationToken = default);
    }
}
