using InventarioBackend.src.Core.Application.Billing.DTOs;

namespace InventarioBackend.src.Core.Application.Billing.Interfaces
{
    public interface IInvoiceCancellationService
    {
        Task<bool> CancelInvoiceAsync(InvoiceCancellationDto cancellationDto, Guid userId);
    }
}
