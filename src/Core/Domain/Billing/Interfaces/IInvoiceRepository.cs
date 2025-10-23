using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Domain.Billing.Entities;

namespace InventarioBackend.Core.Domain.Billing.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetAllAsync();
        Task<List<Invoice>> GetInvoicesByNumberAsync(string number);
        Task<List<Invoice>> GetByEntitiAsync(Guid id);
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<Invoice> AddAsync(Invoice invoice);       
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(Guid id);
        Task<List<Invoice>> GetInvoicesByDateAsync(DateTime date, Guid entitiId);
        Task<List<Invoice>> GetInvoicesByFiltersAsync(SearchInvoiceRequest data, Guid id);
    }
}
