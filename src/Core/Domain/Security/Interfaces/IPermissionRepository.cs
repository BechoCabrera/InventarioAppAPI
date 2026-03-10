using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Domain.Security.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Permission entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
