using InventarioBackend.src.Core.Domain.Billing.Entities;

namespace InventarioBackend.src.Core.Domain.Billing.Interfaces
{
    public interface IInvoiceCancellationRepository
    {
        Task<InvoicesCancelled> AddInvoicesCancelledAsync(InvoicesCancelled cancellationDto);
        Task<List<InvoicesCancelled>> GetAllAsync();
    }
}
