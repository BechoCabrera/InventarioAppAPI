using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using InventarioBackend.src.Core.Domain.Billing.Entities;

namespace InventarioBackend.src.Core.Application.Billing.Services
{
    public class InvoiceCancellationService : IInvoiceCancellationService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICancelledInvoiceRepository _cancelledInvoiceRepository;

        public InvoiceCancellationService(IInvoiceRepository invoiceRepository, ICancelledInvoiceRepository cancelledInvoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
            _cancelledInvoiceRepository = cancelledInvoiceRepository;
        }

        public async Task<bool> CancelInvoiceAsync(InvoiceCancellationDto cancellationDto, Guid userId)
        {
            // Buscar la factura a anular
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(cancellationDto.InvoiceId);
            if (invoice == null || invoice.isCancelled)
            {
                return false; // La factura no existe o ya ha sido anulada
            }

            // Crear un registro de la anulación
            var cancellation = new CancelledInvoice
            {
                InvoiceId = cancellationDto.InvoiceId,
                Reason = cancellationDto.Reason,
                CancellationDate = DateTime.Now,
                CancelledByUserId = userId
            };

            // Marcar la factura como anulada
            invoice.isCancelled = true;
            await _invoiceRepository.UpdateInvoiceAsync(invoice);
            await _cancelledInvoiceRepository.AddCancelledInvoiceAsync(cancellation);

            return true;
        }
    }

}
