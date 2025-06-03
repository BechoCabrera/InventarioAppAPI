using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Settings.Services;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioBackend.Core.Application.Billing.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        private readonly ConsecutiveSettingsService _consecutiveSettingsService;
        public InvoiceService(IInvoiceRepository repository, ConsecutiveSettingsService consecutiveSettingsService)
        {
            _repository = repository;
            _consecutiveSettingsService = consecutiveSettingsService;
        }

        public async Task<List<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _repository.GetAllAsync();
            return invoices.Adapt<List<InvoiceDto>>();
        }

        public async Task<InvoiceDto?> GetByIdAsync(Guid id)
        {
            var invoice = await _repository.GetByIdAsync(id);
            return invoice?.Adapt<InvoiceDto>();
        }

        public async Task AddAsync(InvoiceCreateDto dto)
        {
            var invoice = dto.Adapt<Invoice>();
            dto.InvoiceNumber = await _consecutiveSettingsService.GetNextConsecutiveAsync("ConsecutivoFactura");


            await _repository.AddAsync(invoice);
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
    }
}
