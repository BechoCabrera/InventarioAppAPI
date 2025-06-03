using System;
using System.Threading.Tasks;
using InventarioBackend.Core.Domain.Billing;

namespace InventarioBackend.Core.Domain.Billing.Interfaces
{
    public interface IInvoiceDetailRepository
    {
        Task AddAsync(InvoiceDetail detail);
        Task DeleteAsync(Guid id);
        // Otros métodos según necesidad
    }
}
