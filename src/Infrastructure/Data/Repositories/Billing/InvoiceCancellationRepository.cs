using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Core.Domain.Billing.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Infrastructure.Data.Repositories.Billing
{
    public class InvoiceCancellationRepository : IInvoiceCancellationRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<InvoicesCancelled> _dbSetCancelledInvoice;

        public InvoiceCancellationRepository(AppDbContext context)
        {
            _context = context;
            _dbSetCancelledInvoice = context.Set<InvoicesCancelled>();
        }

        public async Task<InvoicesCancelled> AddInvoicesCancelledAsync(InvoicesCancelled cancellationDto)
        {
            try
            {
                await _dbSetCancelledInvoice.AddAsync(cancellationDto);
                await _context.SaveChangesAsync();
                return cancellationDto;
            }
            catch (DbUpdateException dbEx)
            {
                // Loguear detalles del error, por ejemplo usando un logger.
                throw new Exception($"Error al guardar la anulación: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Error general
                throw new Exception($"Error inesperado: {ex.Message}");
            }
        }

        public async Task<List<InvoicesCancelled>> GetAllAsync()
        {
            return await _dbSetCancelledInvoice
                .Include(i => i.CancelledByUser)
                .Include(i => i.EntitiConfigs)
                .ToListAsync();
        }
    }
}
