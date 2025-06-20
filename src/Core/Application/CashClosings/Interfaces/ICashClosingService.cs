using InventarioBackend.src.Core.Application.CashClosings.DTOs;

namespace InventarioBackend.src.Core.Application.CashClosings.Interfaces
{
    public interface ICashClosingService
    {
        Task<CashClosingDto> CreateAsync(CashClosingCreateDto cashClosingDto, Guid? entitiId, Guid? userId);
        Task<IEnumerable<CashClosingDto>> GetAllAsync(Guid entitiId);
        Task<CashClosingDto> GetByIdAsync(Guid id, Guid entitiId);
        Task DeleteAsync(Guid id, Guid entitiId);
    }
}
