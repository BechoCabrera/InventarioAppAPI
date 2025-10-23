using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioBackend.Infrastructure.Data.Repositories.Billing
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Invoice> _dbSet;
        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Invoice>();
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _dbSet
                .Include(i => i.Client)
                .Include(i => i.Details).ThenInclude(d => d.Product).Include(i => i.EntitiConfigs)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetByEntitiAsync(Guid id)
        {
            try
            {
                return await _dbSet.Where(a => a.EntitiId == id)
               //.Include(i => i.Client).AsSingleQuery()
               //.Include(i => i.EntitiConfigs).AsSingleQuery()
               //.Include(i => i.Details).ThenInclude(d => d.Product).AsSingleQuery().OrderByDescending(a => a.DueDate)
               .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(i => i.Client)
                .Include(i => i.Details).ThenInclude(d => d.Product).Include(i => i.EntitiConfigs)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            try
            {
                await _dbSet.AddAsync(invoice);
                await _context.SaveChangesAsync();
                await _context.Entry(invoice).Reference(i => i.Client).LoadAsync();
                return invoice;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _dbSet.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _dbSet.FindAsync(id);
            if (invoice != null)
            {
                _dbSet.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Invoice>> GetInvoicesByDateAsync(DateTime date, Guid entitiId)
        {
            return await _context.Invoices
                .Where(invoice => invoice.CreatedAt.Date == date.Date && invoice.EntitiId == entitiId && invoice.isCancelled == false)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetInvoicesByNumberAsync(string number)
        {
            return await _context.Invoices
                         .Where(i => i.InvoiceNumber.StartsWith(number) && i.isCancelled == false)
                         .Include(i => i.Client)
                         .Include(i => i.Details).ThenInclude(d => d.Product).Include(i => i.EntitiConfigs)// Filtra por el prefijo del número de factura
                         .ToListAsync();
        }
        public async Task<List<Invoice>> GetInvoicesByFiltersAsync(SearchInvoiceRequest data, Guid entitiId)
        {            
            IQueryable<Invoice> query = _context.Invoices
                .Where(i => i.EntitiId == entitiId)
                .AsNoTracking() // <- Mejora rendimiento (solo lectura)
                .Include(i => i.Client)
                .Include(i => i.Details).ThenInclude(d => d.Product)
                .Include(i => i.EntitiConfigs);

            if (!string.IsNullOrWhiteSpace(data.InvoiceNumber))
                query = query.Where(i => i.InvoiceNumber.StartsWith(data.InvoiceNumber));

            if (data.StartDate.HasValue && data.EndDate.HasValue)
            {
                var start = data.StartDate.Value.Date;
                var end = data.EndDate.Value.Date.AddDays(1).AddTicks(-1); // incluye todo el último día

                query = query.Where(i =>
                    i.IssueDate.Date >= start &&
                    i.IssueDate.Date <= end);
            }
            else if (data.StartDate.HasValue)
                query = query.Where(i => i.IssueDate >= data.StartDate.Value);
            else if (data.EndDate.HasValue)
                query = query.Where(i => i.IssueDate <= data.EndDate.Value);
           
            if (!string.IsNullOrWhiteSpace(data.PaymentMethod))
                query = query.Where(i => i.PaymentMethod != null &&
                                         EF.Functions.Like(i.PaymentMethod.ToLower(), data.PaymentMethod.ToLower()));

            if (data.IsCancelled.HasValue)
                query = query.Where(i => i.isCancelled == data.IsCancelled.Value);

            query = query.OrderByDescending(i => i.IssueDate);
           
            return await query.ToListAsync();
        }
    }
}
