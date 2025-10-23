using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.DTOs;

namespace InventarioBackend.src.Core.Application.Billing.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetAllAsync();
        Task<List<InvoiceDto>> GetByEntitiAsync(Guid id);
        Task<InvoiceDto?> GetByIdAsync(Guid id);
        Task<InvoiceDto> AddAsync(InvoiceCreateDto dto);
        Task<List<InvoiceDto>> GetInvoicesByNumberAsync(string voiceId);
        Task<List<InvoiceDto>> GetInvoicesByFiltersAsync(SearchInvoiceRequest req, Guid entitiId);
        Task UpdateAsync(Guid id, InvoiceCreateDto dto);
        Task DeleteAsync(Guid id);
        Task<List<InvoiceDto>> GetInvoicesByDateAsync(DateTime date, Guid entitiId);

    }
}
