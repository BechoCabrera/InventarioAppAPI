using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;
using InventarioBackend.src.Core.Domain.EntitiConfigs.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Infrastructure.Data.Repositories.EntitiConfigs
{
    public class EntitiConfigRepository : IEntitiConfigRepository
    {
        private readonly AppDbContext _context;

        public EntitiConfigRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EntitiConfig>> GetAllAsync()
        {
            return await _context.EntitiConfigs.ToListAsync();
        }
        public async Task<bool> DeleteAsync(EntitiConfig entity)
        {
            _context.EntitiConfigs.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EntitiConfig?> GetByIdAsync(Guid id)
        {
            return await _context.EntitiConfigs.FirstOrDefaultAsync(x => x.EntitiConfigId == id);
        }
        public async Task<EntitiConfig?> GetByCodeAsync(string code)
        {
            return await _context.EntitiConfigs.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<EntitiConfig> AddAsync(EntitiConfig entity)
        {
            _context.EntitiConfigs.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EntitiConfig> UpdateAsync(EntitiConfig entity)
        {
            var existing = await _context.EntitiConfigs.FindAsync(entity.EntitiConfigId);
            if (existing == null)
                throw new Exception("Entidad no encontrada");

            // Actualiza campo por campo
            existing.Code = entity.Code;
            existing.EntitiName = entity.EntitiName;
            existing.EntitiNit = entity.EntitiNit;
            existing.EntitiAddress = entity.EntitiAddress;
            existing.Description = entity.Description;

            await _context.SaveChangesAsync();
            return existing;
        }
    }

}
