using InventarioBackend.src.Core.Domain.Promotions.Entities;

namespace InventarioBackend.src.Core.Domain.Promotions.Interfaces
{
    public interface IPromotionRepository
    {
        Task AddAsync(Promotion promotion);

        Task<List<Promotion>> GetActiveByEntitiAsync(Guid entitiId);

        Task<List<Promotion>> GetByEntitiAsync(Guid entitiId);

        Task UpdateAsync(Promotion promotion);

        Task<bool> DeleteAsync(Guid id);
    }
}
