using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Application.Products.Interfaces;
using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Products.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace InventarioBackend.src.Core.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Adapt<List<ProductDto>>();
        }
        public async Task<List<ProductDto>> GetByEntitiAsync(Guid entitiId)
        {
            var products = await _productRepository.GetByEntitiAsync(entitiId);
            return products.Adapt<List<ProductDto>>();
        }
        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product?.Adapt<ProductDto>();
        }

        public async Task<Product?> GetByIdDomAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        public async Task<Guid> CreateAsync(ProductCreateDto dto)
        {
            var product = dto.Adapt<Product>();
            string? user = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user != null)
            {
                if (Guid.TryParse(user, out var parsedGuid))
                {
                    product.RegUserId = parsedGuid;
                    product.StockSold = 0;
                }
                await _productRepository.AddAsync(product);
                return product.ProductId;
            }
            else
            {
                throw new Exception("Inicio de sesion finalizada, no se pudo completar la operacion.");
            }

        }

        public async Task<bool> UpdateAsync(ProductUpdateDto product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
            if (existingProduct == null) return false;

            existingProduct.UpdatedAt = DateTime.UtcNow;
            existingProduct.Name = product.Name;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.Description = product.Description;
            existingProduct.Stock = product.Stock + existingProduct.Stock;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.IsActive = product.IsActive;

            await _productRepository.UpdateAsync(existingProduct);
            return true;
        }  
        
        public async Task<bool> UpdateAsync(Product value)
        {
            await _productRepository.UpdateAsync(value);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return false;

            var result = await _productRepository.DeleteAsync(id);
            return result;
        }

        public async Task<List<ProductDto>> SearchByNameAsync(string name, Guid entitiId)
        {
            var products = await _productRepository.SearchByNameAsync(name, entitiId);
            return products.Adapt<List<ProductDto>>();
        }

        public async Task<ProductDto?> GetByBarCodeAsync(string barCode, Guid entitiId)
        {
            var product = await _productRepository.GetByBarCodeAsync(barCode, entitiId);
            return product?.Adapt<ProductDto>();
        }
    }
}
