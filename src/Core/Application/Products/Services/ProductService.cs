﻿using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Application.Products.Interfaces;
using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Products.Interfaces;
using Mapster;
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

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product?.Adapt<ProductDto>();
        }

        public async Task<Guid> CreateAsync(ProductCreateDto dto)
        {
            var product = dto.Adapt<Product>();
            string? user = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(user!= null)
            {
                if (Guid.TryParse(user, out var parsedGuid))
                {
                    product.RegUserId = parsedGuid;
                }
                await _productRepository.AddAsync(product);
                return product.ProductId;
            }
            else
            {
                throw new Exception("Inicio de sesion finalizada, no se pudo completar la operacion.");
            }
           
        }

        public async Task<bool> UpdateStatusAsync(Guid id, bool isActive)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IsActive = isActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _productRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return false;

            await _productRepository.DeleteAsync(id);
            return true;
        }
    }
}
