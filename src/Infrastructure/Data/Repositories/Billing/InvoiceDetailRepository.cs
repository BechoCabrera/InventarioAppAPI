using System;
using System.Threading.Tasks;
using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.Infrastructure.Data.Repositories.Billing
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<InvoiceDetail> _dbSet;

        public InvoiceDetailRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<InvoiceDetail>();
        }

        public async Task AddAsync(InvoiceDetail detail)
        {
            await _dbSet.AddAsync(detail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var detail = await _dbSet.FindAsync(id);
            if (detail != null)
            {
                _dbSet.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
