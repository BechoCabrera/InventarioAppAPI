using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Products.Interfaces;
using InventarioBackend.src.Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Core.Application.Billing.Services
{
    public class InvoiceCancellationService : IInvoiceCancellationService
    {
        private readonly IInvoiceCancellationRepository _invoiceCancellationRepository;
        private readonly IInvoiceRepository _invoiceService;
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;

        // Se agrega AppDbContext para acceder a las transacciones
        public InvoiceCancellationService(IInvoiceCancellationRepository repository,
            IInvoiceRepository invoiceService, IProductRepository productRepository, AppDbContext context)
        {
            _invoiceCancellationRepository = repository;
            _invoiceService = invoiceService;
            _productRepository = productRepository;
            _context = context;  // Inyección del contexto de base de datos
        }

        public async Task<bool> AddInvoicesCancelledAsync(InvoiceCancellationDto cancellationDto, Guid userId)
        {
            var strategy = _context.Database.CreateExecutionStrategy(); // Crear la estrategia de ejecución

            // Ejecuta las operaciones dentro de la estrategia de ejecución
            return await strategy.ExecuteAsync(async () =>
            {
                // Inicia una transacción
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    Invoice? invoice = await _invoiceService.GetByIdAsync(cancellationDto.InvoiceId);
                    if (invoice == null || invoice.isCancelled)
                    {
                        // Si la factura no existe o ya ha sido anulada, retornamos falso
                        return false;
                    }

                    // Crear la entidad de anulación de factura
                    InvoicesCancelled cancellation = new InvoicesCancelled
                    {
                        InvoiceId = cancellationDto.InvoiceId,
                        Reason = cancellationDto.Reason,
                        CancellationDate = DateTime.Now,
                        CancelledByUserId = userId,
                        EntitiConfigId = cancellationDto.EntitiConfigId
                    };

                    // Marcar la factura como anulada
                    invoice.isCancelled = true;

                    // Actualizar el stock de los productos
                    foreach (InvoiceDetail item in invoice.Details)
                    {
                        Product? dataProdut = await _productRepository.GetByIdAsync(item.ProductId);
                        if (dataProdut != null)
                        {
                            dataProdut.StockSold = dataProdut.StockSold - item.Quantity;
                            if(dataProdut.StockSold < 0)
                            {
                                throw new Exception("Error en catidad del producto " + dataProdut.Name);
                            }
                        }
                    }

                    // Guardar la anulación de la factura en la base de datos
                    await _invoiceCancellationRepository.AddInvoicesCancelledAsync(cancellation);

                    // Actualizar la factura para marcarla como anulada
                    await _invoiceService.UpdateAsync(invoice);

                    // Confirmar la transacción si todo es exitoso
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    // Si ocurre cualquier error, hacer rollback
                    await transaction.RollbackAsync();

                    // Opcional: loguear el error
                    // Log.Error(ex, "Error al procesar la anulación de factura");

                    return false;
                }
            });
        }


        public async Task<List<InvoiceCancellationDto>> GetAllAsync(Guid entitiId)
        {
            try
            {
                List<InvoicesCancelled> invoices = await _invoiceCancellationRepository.GetAllAsync(entitiId);
                return invoices.Adapt<List<InvoiceCancellationDto>>();
            }
            catch (Exception ex)
            {
                
                return new List<InvoiceCancellationDto>();
            }
            
        }
    }
}
