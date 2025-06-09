using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Domain.Products.Entities;

namespace InventarioBackend.src.Core.Domain.Products.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<List<CategoryDto>> GetByEntitiAsync(Guid id);
        Task<CategoryDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(Category category);
        Task<bool> UpdateAsync(Guid id, CategoryUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
