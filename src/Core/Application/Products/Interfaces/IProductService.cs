using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioBackend.src.Core.Application.Products.DTOs;

namespace InventarioBackend.src.Core.Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(ProductCreateDto dto);
        //Task<ProductDto?> UpdateAsync(Guid id, ProductDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateStatusAsync(Guid id, bool isActive);
        Task<List<ProductDto>> SearchByNameAsync(string name);
        Task<ProductDto?> GetByBarCodeAsync(string barCode);
    }

}
