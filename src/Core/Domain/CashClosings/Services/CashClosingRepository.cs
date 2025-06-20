
using InventarioBackend.src.Core.Domain.CashClosings.Entities;
using InventarioBackend.src.Core.Domain.CashClosings.Interfaces;
using InventarioBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Core.Domain.CashClosings.Services
{
    public class CashClosingRepository : ICashClosingRepository
    {
        private readonly AppDbContext _context;

        public CashClosingRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los cierres de caja filtrados por EntitiId
        public async Task<IEnumerable<CashClosing>> GetAllAsync(Guid entitiId)
        {
            return await _context.CashClosings
                                 .Where(c => c.EntitiId == entitiId)
                                 .ToListAsync();
        }

        // Obtener un cierre de caja por ID y EntitiId
        public async Task<CashClosing> GetByIdAsync(Guid id, Guid entitiId)
        {
            return await _context.CashClosings
                                 .FirstOrDefaultAsync(c => c.CashClosingId == id && c.EntitiId == entitiId);
        }

        // Agregar un nuevo cierre de caja
        public async Task AddAsync(CashClosing cashClosing)
        {
            await _context.CashClosings.AddAsync(cashClosing);
        }

        // Eliminar un cierre de caja por ID y EntitiId
        public async Task DeleteAsync(Guid id, Guid entitiId)
        {
            var cashClosing = await _context.CashClosings
                                            .FirstOrDefaultAsync(c => c.CashClosingId == id && c.EntitiId == entitiId);
            if (cashClosing != null)
            {
                _context.CashClosings.Remove(cashClosing);
            }
        }

        // Guardar cambios en la base de datos
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
