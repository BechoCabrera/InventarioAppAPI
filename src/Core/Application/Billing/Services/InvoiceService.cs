using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Products.Services;
using InventarioBackend.src.Core.Application.Promotions.Interfaces;
using InventarioBackend.src.Core.Application.Settings.Services;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Core.Domain.Products;
using Mapster;
using InventarioBackend.src.Core.Application.Promotions.DTOs;
using InventarioBackend.src.Infrastructure.Data;
using InventarioBackend.src.Core.Domain.Products.Interfaces;

namespace InventarioBackend.src.Core.Application.Billing.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        private readonly IPromotionService _promotionService;
        private readonly ConsecutiveSettingsService _consecutiveSettingsService;
        private readonly ProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;
        public InvoiceService(IInvoiceRepository repository, ConsecutiveSettingsService consecutiveSettingsService,
            ProductService productService,
             IPromotionService promotionService, AppDbContext context, IProductRepository productRepository)
        {
            _productService = productService;
            _repository = repository;
            _consecutiveSettingsService = consecutiveSettingsService;
            _promotionService = promotionService;
            _context = context;
            _productRepository = productRepository;

        }

        public async Task<List<InvoiceDto>> GetAllAsync()
        {
            try
            {
                var invoices = await _repository.GetAllAsync();
                return invoices.Adapt<List<InvoiceDto>>();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task<List<InvoiceDto>> GetByEntitiAsync(Guid id)
        {
            List<Invoice> invoices = await _repository.GetByEntitiAsync(id);
            return invoices.Adapt<List<InvoiceDto>>();
        }

        public async Task<InvoiceDto?> GetByIdAsync(Guid id)
        {
            var invoice = await _repository.GetByIdAsync(id);
            return invoice?.Adapt<InvoiceDto>();
        }

        public async Task UpdateAsync(Guid id, InvoiceCreateDto dto)
        {
            try
            {
                var invoice = await _repository.GetByIdAsync(id);
                if (invoice == null) return;

                dto.Adapt(invoice);
                invoice.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(invoice);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InvoiceDto> AddAsync(InvoiceCreateDto dto)
        {
            var invoice = dto.Adapt<Invoice>();

            invoice.InvoiceNumber = await _consecutiveSettingsService
                .GetNextConsecutiveAsync("ConsecutivoFactura");

            invoice.DueDate = DateTime.Now;
            invoice.IssueDate = DateTime.Now;
            invoice.CreatedAt = DateTime.Now;

            invoice.NameClientDraft = invoice.NameClientDraft != null
                ? invoice.NameClientDraft.ToUpper()
                : null;

            var subtotal = invoice.Details.Sum(d => d.Quantity * d.UnitPrice);

            var promotionItems = invoice.Details.Select(d => new CartItemDto
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice
            }).ToList();

            CalculatePromotionResponseDto? promotionResult = null;
            if (invoice.EntitiId.HasValue)
            {
                promotionResult = await _promotionService
                    .CalculateAsync(promotionItems, invoice.EntitiId.Value);
            }

            var discount = promotionResult?.DiscountAmount ?? 0;
            var promotionName = promotionResult?.PromotionName;

            var subtotalWithDiscount = subtotal - discount;

            invoice.SubtotalAmount = subtotal;
            invoice.TaxAmount = subtotalWithDiscount * 0.19m; // IVA
            invoice.TotalAmount = subtotalWithDiscount + invoice.TaxAmount;

            // Ejecutar las actualizaciones de stock y creación de factura dentro de una transacción
            Invoice savedInvoice = null!;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Validar y actualizar StockSold de cada producto
                foreach (var item in invoice.Details)
                {
                    Product? valueProduct = await _productRepository.GetByIdAsync(item.ProductId);

                    if (valueProduct == null)
                    {
                        throw new InvalidOperationException($"Producto no encontrado. Id: {item.ProductId}");
                    }

                    var available = valueProduct.Stock - valueProduct.StockSold;
                    if (available < item.Quantity)
                    {
                        throw new InvalidOperationException($"No hay suficiente stock para el producto '{valueProduct.Name}'. Disponible: {available}, solicitado: {item.Quantity}");
                    }

                    valueProduct.StockSold += item.Quantity;
                    await _productRepository.UpdateAsync(valueProduct);
                }

                // Guardar la factura
                savedInvoice = await _repository.AddAsync(invoice);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return savedInvoice.Adapt<InvoiceDto>();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<InvoiceDto>> GetInvoicesByDateAsync(DateTime date, Guid entitiId)
        {
            var invoices = await _repository.GetInvoicesByDateAsync(date, entitiId);

            return invoices.Adapt<List<InvoiceDto>>();
        }
        public async Task<List<InvoiceDto>> GetInvoicesByNumberAsync(string number)
        {
            var result = await _repository.GetInvoicesByNumberAsync(number);
            return result.Adapt<List<InvoiceDto>>();
        } 
        public async Task<List<InvoiceDto>> GetInvoicesByFiltersAsync(SearchInvoiceRequest data, Guid entitiId)
        {
            var result = await _repository.GetInvoicesByFiltersAsync(data, entitiId);
            return result.Adapt<List<InvoiceDto>>();
        }
    }
}
