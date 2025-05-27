using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Application.Products.Interfaces;
using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Products.Interfaces;
using Mapster;

namespace InventarioBackend.src.Core.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }  

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Adapt<List<ProductDto>>();
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product?.Adapt<ProductDto>();
        }

        public async Task<Guid> CreateAsync(ProductCreateDto dto)
        {
            var product = dto.Adapt<Product>();
            await _productRepository.AddAsync(product);
            return product.ProductId;
        }

        //public async Task<bool> UpdateAsync(Guid id, ProductDto dto)
        //{
        //    var existing = await _productRepository.GetByIdAsync(id);
        //    if (existing == null) return false;

        //    dto.Adapt(existing);
        //    existing.UpdatedAt = DateTime.UtcNow;

        //    await _productRepository.UpdateAsync(existing);
        //    return true;
        //}

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return false;

            await _productRepository.DeleteAsync(id);
            return true;
        }
    }
}
