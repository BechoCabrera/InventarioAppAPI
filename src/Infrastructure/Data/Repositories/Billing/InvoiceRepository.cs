using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
                .Include(i => i.Details).ThenInclude(d => d.Product)
                .ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(i => i.Client)
                .Include(i => i.Details).ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _dbSet.AddAsync(invoice);
            await _context.SaveChangesAsync();
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
    }
}
