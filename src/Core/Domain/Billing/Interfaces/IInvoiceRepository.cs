using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.src.Core.Domain.Billing.Entities;

namespace InventarioBackend.Core.Domain.Billing.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetAllAsync();
        Task<List<Invoice>> GetByEntitiAsync(Guid id);
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<Invoice> AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(Guid id);
    }
}
