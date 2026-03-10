using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Application.Products.Interfaces;
using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Products.Interfaces;
using InventarioBackend.src.Core.Domain.Promotions.Interfaces;
using InventarioBackend.src.Infrastructure.Data.Repositories.Products;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace InventarioBackend.src.Core.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        private readonly IPromotionRepository _promotionRepository;

        public ProductService(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, IPromotionRepository promotionRepository)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
            _promotionRepository = promotionRepository;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Adapt<List<ProductDto>>();
        }
        //public async Task<List<ProductDto>> GetByEntitiAsync(Guid entitiId)
        //{
        //    var products = await _productRepository.GetByEntitiAsync(entitiId);
        //    return products.Adapt<List<ProductDto>>();
        //}

        public async Task<List<ProductDto>> GetByEntitiAsync(Guid entitiId)
        {
            var products = await _productRepository.GetByEntitiAsync(entitiId);

            // Inyecta el repositorio de promociones en el servicio
            var promociones = await _promotionRepository.GetActiveByEntitiAsync(entitiId);

            var result = products.Select(product => new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                StockSold = product.StockSold,
                Category = product.Category?.Name,
                IsActive = product.IsActive,
                RegUserId = product.RegUserId,
                EntitiId = product.EntitiId,
                BarCode = product.BarCode ?? string.Empty,
                EntitiName = product.EntitiConfigs?.EntitiName,
                Username = product.User?.Name,
                CategoryName = product.Category?.Name,
                Discounts = promociones
                    .Where(promo => promo.PromotionProducts.Any(pp => pp.ProductId == product.ProductId))
                    .Select(promo => new ProductDiscountDto
                    {
                        PromotionId = promo.PromotionId,
                        PromotionName = promo.Name,
                        Percentage = promo.Percentage,
                        StartDate = promo.StartDate,
                        EndDate = promo.EndDate,
                        IsActive = promo.IsActive
                    }).ToList()
            }).ToList();

            return result;
        }


        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            var entitiId = product.EntitiId ?? Guid.Empty;
            var promociones = await _promotionRepository.GetActiveByEntitiAsync(entitiId);

            var dto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                StockSold = product.StockSold,
                Category = product.Category?.Name,
                IsActive = product.IsActive,
                RegUserId = product.RegUserId,
                EntitiId = product.EntitiId,
                BarCode = product.BarCode ?? string.Empty,
                EntitiName = product.EntitiConfigs?.EntitiName,
                Username = product.User?.Name,
                CategoryName = product.Category?.Name,
                Discounts = promociones
                    .Where(promo => promo.PromotionProducts.Any(pp => pp.ProductId == product.ProductId))
                    .Select(promo => new ProductDiscountDto
                    {
                        PromotionId = promo.PromotionId,
                        PromotionName = promo.Name,
                        Percentage = promo.Percentage,
                        StartDate = promo.StartDate,
                        EndDate = promo.EndDate,
                        IsActive = promo.IsActive
                    }).ToList()
            };

            return dto;
        }


        public async Task<ProductDto?> GetByIdDomAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            var entitiId = product.EntitiId ?? Guid.Empty;
            var promociones = await _promotionRepository.GetActiveByEntitiAsync(entitiId);

            var dto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                StockSold = product.StockSold,
                Category = product.Category?.Name,
                IsActive = product.IsActive,
                RegUserId = product.RegUserId,
                EntitiId = product.EntitiId,
                BarCode = product.BarCode ?? string.Empty,
                EntitiName = product.EntitiConfigs?.EntitiName,
                Username = product.User?.Name,
                CategoryName = product.Category?.Name,
                Discounts = promociones
                    .Where(promo => promo.PromotionProducts.Any(pp => pp.ProductId == product.ProductId))
                    .Select(promo => new ProductDiscountDto
                    {
                        PromotionId = promo.PromotionId,
                        PromotionName = promo.Name,
                        Percentage = promo.Percentage,
                        StartDate = promo.StartDate,
                        EndDate = promo.EndDate,
                        IsActive = promo.IsActive
                    }).ToList()
            };

            return dto;
        }


        public async Task<Guid> CreateAsync(ProductCreateDto dto)
        {
            try
            {
                var product = dto.Adapt<Product>();
                string? user = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (user != null)
                {
                    if (Guid.TryParse(user, out var parsedGuid))
                    {
                        product.RegUserId = parsedGuid;
                        product.StockSold = dto.Stock;
                    }
                    product.Name = dto.Name.Trim();

                    var varCodeExist = await _productRepository.GetByBarCodeAsync(dto.BarCode, dto.EntitiId.Value);
                    if (varCodeExist != null)
                        throw new Exception("No es posible guardar este producto, este codigo de barra ya existe con el nombre de: " + varCodeExist.Name + " - " + varCodeExist.BarCode);

                    await _productRepository.AddAsync(product);
                    return product.ProductId;
                }
                else
                {
                    throw new Exception("Inicio de sesion finalizada, no se pudo completar la operacion.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task<bool> UpdateAsync(ProductUpdateDto product, Guid entitiId)
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.ProductId);
            if (existingProduct == null) return false;

            var barCodExit = await _productRepository.GetByBarCodeAsync(product.BarCode, entitiId);

            if (barCodExit != null && product.ProductId != barCodExit.ProductId)
                throw new Exception("El Cod:" + barCodExit.BarCode + " Ya existe. lo tiene el producto " + barCodExit.Name);

            existingProduct.UpdatedAt = DateTime.UtcNow;
            existingProduct.Name = product.Name;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.Description = product.Description;
            existingProduct.Stock = existingProduct.Stock;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.IsActive = product.IsActive;
            existingProduct.BarCode = product.BarCode;

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
            var promociones = await _promotionRepository.GetActiveByEntitiAsync(entitiId);

            var result = products.Select(product => new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                StockSold = product.StockSold,
                Category = product.Category?.Name,
                IsActive = product.IsActive,
                RegUserId = product.RegUserId,
                EntitiId = product.EntitiId,
                BarCode = product.BarCode ?? string.Empty,
                EntitiName = product.EntitiConfigs?.EntitiName,
                Username = product.User?.Name,
                CategoryName = product.Category?.Name,
                Discounts = promociones
                    .Where(promo => promo.PromotionProducts.Any(pp => pp.ProductId == product.ProductId))
                    .Select(promo => new ProductDiscountDto
                    {
                        PromotionId = promo.PromotionId,
                        PromotionName = promo.Name,
                        Percentage = promo.Percentage,
                        StartDate = promo.StartDate,
                        EndDate = promo.EndDate,
                        IsActive = promo.IsActive
                    }).ToList()
            }).ToList();

            return result;
        }


        public async Task<ProductDto?> GetByBarCodeAsync(string barCode, Guid entitiId)
        {
            var product = await _productRepository.GetByBarCodeAsync(barCode, entitiId);
            if (product == null) return null;

            var promociones = await _promotionRepository.GetActiveByEntitiAsync(entitiId);

            var dto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                StockSold = product.StockSold,
                Category = product.Category?.Name,
                IsActive = product.IsActive,
                RegUserId = product.RegUserId,
                EntitiId = product.EntitiId,
                BarCode = product.BarCode ?? string.Empty,
                EntitiName = product.EntitiConfigs?.EntitiName,
                Username = product.User?.Name,
                CategoryName = product.Category?.Name,
                Discounts = promociones
                    .Where(promo => promo.PromotionProducts.Any(pp => pp.ProductId == product.ProductId))
                    .Select(promo => new ProductDiscountDto
                    {
                        PromotionId = promo.PromotionId,
                        PromotionName = promo.Name,
                        Percentage = promo.Percentage,
                        StartDate = promo.StartDate,
                        EndDate = promo.EndDate,
                        IsActive = promo.IsActive
                    }).ToList()
            };

            return dto;
        }


        public async Task<ProductDto?> IncreaseStockAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return null;
            }
            product.Stock += quantity;

            await _productRepository.UpdateAsync(product);
            return product?.Adapt<ProductDto>();
        }

        public async Task<ProductDto?> DecreaseStockAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return null;
            }
            if ((product.Stock - product.StockSold) < quantity)
            {
                throw new InvalidOperationException("No hay suficiente stock para reducir.");
            }

            product.Stock -= quantity;

            await _productRepository.UpdateAsync(product);
            return product?.Adapt<ProductDto>();

        }

    }
}
