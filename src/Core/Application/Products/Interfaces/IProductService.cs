using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioBackend.src.Core.Application.Products.DTOs;

namespace InventarioBackend.src.Core.Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task AddAsync(ProductDto productDto);
        Task UpdateAsync(ProductDto productDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
