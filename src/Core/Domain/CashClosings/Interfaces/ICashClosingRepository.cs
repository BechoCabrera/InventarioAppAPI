using InventarioBackend.src.Core.Domain.CashClosings.Entities;

namespace InventarioBackend.src.Core.Domain.CashClosings.Interfaces
{
    public interface ICashClosingRepository
    {
        Task<IEnumerable<CashClosing>> GetAllAsync(Guid entitiId);
        Task<CashClosing> GetByIdAsync(Guid id, Guid entitiId);
        Task AddAsync(CashClosing cashClosing);
        Task DeleteAsync(Guid id, Guid entitiId);
        Task SaveChangesAsync();
    }
}
