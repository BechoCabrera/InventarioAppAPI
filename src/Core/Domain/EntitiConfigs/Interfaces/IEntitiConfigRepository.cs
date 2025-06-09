
using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;

namespace InventarioBackend.src.Core.Domain.EntitiConfigs.Interfaces
{
    public interface IEntitiConfigRepository
    {
        Task<EntitiConfig?> GetByCodeAsync(string code);
        Task<EntitiConfig?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(EntitiConfig id);
        Task<List<EntitiConfig>> GetAllAsync();
        Task<EntitiConfig> AddAsync(EntitiConfig entity);
        Task<EntitiConfig> UpdateAsync(EntitiConfig entity);
    }
}
