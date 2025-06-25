using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.Infrastructure.Data.Repositories.Billing;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Application.Products.Services;
using InventarioBackend.src.Core.Application.Settings.Services;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Core.Domain.Products;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventarioBackend.Core.Application.Billing.Services
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
            var invoices = await _repository.GetAllAsync();
            return invoices.Adapt<List<InvoiceDto>>();
        }
        public async Task<List<InvoiceDto>> GetByEntitiAsync(Guid id)
        {
            var invoices = await _repository.GetByEntitiAsync(id);
            return invoices.Adapt<List<InvoiceDto>>();
        }

        public async Task<InvoiceDto?> GetByIdAsync(Guid id)
        {
            var invoice = await _repository.GetByIdAsync(id);
            return invoice?.Adapt<InvoiceDto>();
        }

        public async Task<InvoiceDto> AddAsync(InvoiceCreateDto dto)
        {
            return dto.Adapt<InvoiceDto>();
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
            var invoice = await _repository.GetByIdAsync(id);
            if (invoice == null) return;

            dto.Adapt(invoice);
            invoice.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(invoice);
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

        public async Task<bool> CancelInvoiceAsync(InvoiceCancellationDto cancellationDto, Guid userId)
        {
            try
            {
                // Buscar la factura a anular
                var invoice = await _repository.GetByIdAsync(cancellationDto.InvoiceId);
                if (invoice == null || invoice.isCancelled)
                {
                    //return false; // La factura no existe o ya ha sido anulada
                }

                // Crear un registro de la anulación
                CancelledInvoice cancellation = new CancelledInvoice
                {
                    InvoiceId = cancellationDto.InvoiceId,
                    Reason = cancellationDto.Reason,
                    CancellationDate = DateTime.Now,
                    CancelledByUserId = userId,
                };

                // Marcar la factura como anulada
                invoice.isCancelled = true;                
                await _repository.AddCancelledInvoiceAsync(cancellation);
                await _repository.UpdateAsync(invoice);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<InvoiceDto>> GetInvoicesByNumberAsync(string number)
        {
            var result = await _repository.GetInvoicesByNumberAsync(number);
            return result.Adapt<List<InvoiceDto>>();
        }
    }
}
