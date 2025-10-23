using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Products.Services;
using InventarioBackend.src.Core.Application.Settings.Services;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Core.Domain.Products;
using Mapster;

namespace InventarioBackend.src.Core.Application.Billing.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        private readonly ConsecutiveSettingsService _consecutiveSettingsService;
        private readonly ProductService _productService;
        public InvoiceService(IInvoiceRepository repository, ConsecutiveSettingsService consecutiveSettingsService,
            ProductService productService)
        {
            _productService = productService;
            _repository = repository;
            _consecutiveSettingsService = consecutiveSettingsService;
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

        public async Task<InvoiceDto> AddAsync(InvoiceCreateDto dto)
        {
            var invoice = dto.Adapt<Invoice>();
            invoice.InvoiceNumber = await _consecutiveSettingsService.GetNextConsecutiveAsync("ConsecutivoFactura");
            invoice.DueDate = DateTime.Now;
            invoice.IssueDate = DateTime.Now;
            invoice.CreatedAt = DateTime.Now;
            invoice.NitClientDraft = invoice.NitClientDraft;
            invoice.NameClientDraft = invoice.NameClientDraft != null ? invoice.NameClientDraft.ToUpper() : null;
            foreach (var item in invoice.Details)
            {
                Product? valueProduct = await _productService.GetByIdDomAsync(item.ProductId);
                if (valueProduct != null && valueProduct.Stock >= valueProduct.StockSold)
                {
                    valueProduct.StockSold = item.Quantity + valueProduct.StockSold;
                    await _productService.UpdateAsync(valueProduct);
                }
            }

            var savedInvoice = await _repository.AddAsync(invoice);

            return savedInvoice.Adapt<InvoiceDto>();
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
