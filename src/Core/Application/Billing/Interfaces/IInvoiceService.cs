using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.DTOs;

namespace InventarioBackend.src.Core.Application.Billing.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetAllAsync();
        Task<List<InvoiceDto>> GetByEntitiAsync(Guid id);
        Task<InvoiceDto?> GetByIdAsync(Guid id);
        Task AddAsync(InvoiceCreateDto dto);
        Task UpdateAsync(Guid id, InvoiceCreateDto dto);
        Task DeleteAsync(Guid id);
    }
}
