using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Application.Billing.Services;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.Infrastructure.Data.Repositories.Billing;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Infrastructure.Data.Repositories.Billing;
using Mapster;
using System;
using System.Threading.Tasks;

namespace InventarioBackend.src.Core.Application.Billing.Services
{
    public class InvoiceCancellationService : IInvoiceCancellationService
    {
        private readonly IInvoiceCancellationRepository _invoiceCancellationRepository;
        private readonly InvoiceService _invoiceRepository;

        public InvoiceCancellationService(IInvoiceCancellationRepository repository, InvoiceService invoiceRepository)
        {
            _invoiceCancellationRepository = repository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<bool> AddInvoicesCancelledAsync(InvoiceCancellationDto cancellationDto, Guid userId)
        {
            try
            {
                // Buscar la factura a anular
                InvoiceDto? invoice = await _invoiceRepository.GetByIdAsync(cancellationDto.InvoiceId);
                if (invoice == null || invoice.isCancelled)
                {
                    // Si la factura no existe o ya ha sido anulada, retornamos falso
                    return false;
                }

                // Crear un registro de la anulación
                InvoicesCancelled cancellation = new InvoicesCancelled
                {
                    InvoiceId = cancellationDto.InvoiceId,
                    Reason = cancellationDto.Reason,
                    CancellationDate = DateTime.Now,
                    CancelledByUserId = userId,
                };

                // Marcar la factura como anulada
                invoice.isCancelled = true;

                // Guardar la anulación en la base de datos
                await _invoiceCancellationRepository.AddInvoicesCancelledAsync(cancellation);

                // Actualizar la factura
                await _invoiceRepository.UpdateAsync(invoice.InvoiceId, invoice.Adapt<InvoiceCreateDto>());

                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, podrías loguearlo si es necesario
                return false;
            }
        }

        public async Task<List<InvoiceCancellationDto>> GetAllAsync()
        {
            List<InvoicesCancelled> invoices = await _invoiceCancellationRepository.GetAllAsync();
            return invoices.Adapt<List<InvoiceCancellationDto>>();
        }
    }
}
