using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioBackend.Core.Domain.Billing.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetAllAsync();
        Task<List<Invoice>> GetByEntitiAsync(Guid id);
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<Invoice> AddAsync(Invoice invoice);
        Task<CancelledInvoice> AddCancelledInvoiceAsync(CancelledInvoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(Guid id);
        Task<List<Invoice>> GetInvoicesByDateAsync(DateTime date, Guid entitiId);
    }
}
