using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Infrastructure.Data.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Product> _dbSet;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Product>();
    }
    public async Task<List<Product>> GetByEntitiAsync(Guid entitiId)
    {
        return await _context.Products
                             .Where(p => p.EntitiId == entitiId)
                             .Include(x => x.EntitiConfigs)
                             .Include(a => a.Category)
                             .Include(a => a.User)
                             .ToListAsync();
    }
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet.Include(p => p.User).Include(a => a.Category).Include(p => p.EntitiConfigs).ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _dbSet.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _dbSet.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<List<Product>> SearchByNameAsync(string name, Guid entitiId)
    {

        return await _context.Products.Include(p => p.EntitiConfigs)
            .Where(p => p.Name.Contains(name) && p.EntitiId == entitiId)
            .ToListAsync();
    }

    public async Task<Product?> GetByBarCodeAsync(string barCode, Guid entitiId)
    {
        return await _context.Products.Include(p => p.EntitiConfigs)
            .FirstOrDefaultAsync(p => p.BarCode == barCode && p.EntitiId == entitiId);
    }
}
