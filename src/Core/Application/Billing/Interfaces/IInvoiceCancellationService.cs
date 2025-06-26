using InventarioBackend.src.Core.Application.Billing.DTOs;

namespace InventarioBackend.src.Core.Application.Billing.Interfaces
{
    public interface IInvoiceCancellationService
    {
        Task<bool> AddInvoicesCancelledAsync(InvoiceCancellationDto cancellationDto, Guid userId);
        Task<List<InvoiceCancellationDto>> GetAllAsync();
    }
}
