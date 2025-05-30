using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Domain.Products.Entities;

namespace InventarioBackend.src.Core.Application.Products.Interfaces
{
    public interface ICategoryRepository
    {
         Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
    }
}
