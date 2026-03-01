using InventarioBackend.src.Core.Domain.Promotions.Entities;
using InventarioBackend.src.Core.Domain.Promotions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Infrastructure.Data.Repositories.Promotions
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Promotion> _dbSet;

        public PromotionRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Promotion>();
        }

        public async Task AddAsync(Promotion promotion)
        {
            await _dbSet.AddAsync(promotion);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Promotion>> GetByEntitiAsync(Guid entitiId)
        {
            return await _context.Set<Promotion>()
                .Where(p => p.EntitiId == entitiId)
                .Include(p => p.PromotionProducts)
                .ToListAsync();
        }

        public async Task<List<Promotion>> GetActiveByEntitiAsync(Guid entitiId)
        {
            var now = DateTime.UtcNow;

            return await _context.Set<Promotion>()
                .Where(p =>
                    p.EntitiId == entitiId &&
                    p.IsActive &&
                    p.StartDate <= now &&
                    p.EndDate >= now)
                .Include(p => p.PromotionProducts)
                .ToListAsync();
        }

        public async Task UpdateAsync(Promotion promotion)
        {
            _context.Update(promotion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var promotion = await _context.Promotions
                .FindAsync(id);

            if (promotion == null)
                return false;

            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}