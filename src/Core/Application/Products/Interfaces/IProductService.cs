using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioBackend.src.Core.Application.Products.DTOs;

namespace InventarioBackend.src.Core.Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<List<ProductDto>> GetByEntitiAsync(Guid id);
        Task<Guid> CreateAsync(ProductCreateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(ProductUpdateDto product);
        Task<List<ProductDto>> SearchByNameAsync(string name, Guid entitiId);
        Task<ProductDto?> GetByBarCodeAsync(string barCode, Guid entitiId);
    }

}
