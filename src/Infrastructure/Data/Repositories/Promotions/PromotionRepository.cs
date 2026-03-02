using InventarioBackend.src.Core.Domain.Promotions.Entities;
using InventarioBackend.src.Core.Domain.Promotions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Infrastructure.Data.Repositories.Promotions
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Promotion> _dbSet;
        private readonly DbSet<PromotionProduct> _dbSetPromotionProducts;

        public PromotionRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Promotion>();
            _dbSetPromotionProducts = context.Set<PromotionProduct>();
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
                .Include(p => p.PromotionProducts).ThenInclude(pp => pp.Product)
                .ToListAsync();
        }

        public async Task<List<Promotion>> GetActiveByEntitiAsync(Guid entitiId)
        {
            var now = DateTime.UtcNow;

            return await _context.Set<Promotion>()
                .Where(p =>
                    p.EntitiId == entitiId &&
                    p.IsActive &&
                    p.StartDate.Date <= now.Date &&
                    p.EndDate.Date >= now.Date)
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
                .Include(p => p.PromotionProducts)
                .FirstOrDefaultAsync(p => p.PromotionId == id);

            if (promotion == null)
                return false;

            // Elimina los productos asociados a la promoción
            if (promotion.PromotionProducts != null && promotion.PromotionProducts.Any())
            {
                _context.PromotionProducts.RemoveRange(promotion.PromotionProducts);
            }

            // Elimina la promoción
            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}