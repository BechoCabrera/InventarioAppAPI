using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Application.Products.Interfaces;
using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Products.Interfaces;

namespace InventarioBackend.src.Core.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => MapToDto(p));
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : MapToDto(product);
        }

        public async Task AddAsync(ProductDto productDto)
        {
            var product = MapToEntity(productDto);
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            var product = MapToEntity(productDto);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null;
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        private static Product MapToEntity(ProductDto dto)
        {
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }
    }
}
