using InventarioBackend.src.Core.Application.Promotions.DTOs;
using InventarioBackend.src.Core.Application.Promotions.Interfaces;
using InventarioBackend.src.Core.Domain.Promotions.Entities;
using InventarioBackend.src.Core.Domain.Promotions.Interfaces;
using Mapster;

namespace InventarioBackend.src.Core.Application.Promotions.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PromotionService(
            IPromotionRepository promotionRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _promotionRepository = promotionRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> CreateAsync(PromotionCreateDto dto)
        {
            try
            {
                var entitiIdClaim = _httpContextAccessor.HttpContext?
             .User.FindFirst("entiti_id")?.Value;

                if (string.IsNullOrEmpty(entitiIdClaim))
                    throw new Exception("Entidad no válida.");

                Guid entitiId = Guid.Parse(entitiIdClaim);

                Promotion promotion = dto.Adapt<Promotion>();
                promotion.PromotionId = Guid.NewGuid();
                promotion.EntitiId = entitiId;
                promotion.IsActive = dto.IsActive;

                promotion.PromotionProducts = dto.ProductIds
                    .Select(p => new PromotionProduct
                    {
                        PromotionId = promotion.PromotionId,
                        ProductId = p
                    }).ToList();

                await _promotionRepository.AddAsync(promotion);

                return promotion.PromotionId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<CalculatePromotionResponseDto> CalculateAsync(
    List<CartItemDto> items,
    Guid entitiId)
        {
            var promotions = await _promotionRepository.GetActiveByEntitiAsync(entitiId);

            decimal bestDiscount = 0;
            string? bestPromotion = null;
            decimal bestPercentage = 0;

            foreach (var promo in promotions)
            {
                decimal discount = 0;

                // ProductPercentage
                if (promo.Type == 1)
                {
                    foreach (var item in items)
                    {
                        if (promo.PromotionProducts.Any(p => p.ProductId == item.ProductId))
                        {
                            discount += item.UnitPrice * item.Quantity *
                                (promo.Percentage / 100);
                        }
                    }
                }

                // QuantityPercentage
                if (promo.Type == 3)
                {
                    foreach (var item in items)
                    {
                        if (promo.PromotionProducts.Any(p => p.ProductId == item.ProductId) &&
                            item.Quantity >= promo.MinQuantity)
                        {
                            discount += item.UnitPrice * item.Quantity *
                                (promo.Percentage / 100);
                        }
                    }
                }

                // ComboPercentage
                if (promo.Type == 2)
                {
                    var requiredProducts = promo.PromotionProducts
                        .Select(x => x.ProductId).ToList();

                    if (requiredProducts.All(rp => items.Any(i => i.ProductId == rp)))
                    {
                        var comboSubtotal = items
                            .Where(i => requiredProducts.Contains(i.ProductId))
                            .Sum(i => i.UnitPrice);

                        discount = comboSubtotal * (promo.Percentage / 100);
                    }
                }

                if (discount > bestDiscount)
                {
                    bestDiscount = discount;
                    bestPromotion = promo.Name;
                    bestPercentage = promo.Percentage; // Guarda el porcentaje de la mejor promo
                }
            }

            return new CalculatePromotionResponseDto
            {
                DiscountAmount = bestDiscount,
                PromotionName = bestPromotion,
                Percentage = bestPercentage // Retorna el porcentaje aplicado
            };
        }

        public async Task<List<PromotionDto>> GetByEntitiAsync(Guid entitiId)
        {
            List<Promotion> promotions = await _promotionRepository.GetByEntitiAsync(entitiId);

            var result = promotions.Select(p => new PromotionDto
            {
                PromotionId = p.PromotionId,
                Name = p.Name,
                Type = p.Type,
                Percentage = p.Percentage,
                MinQuantity = p.MinQuantity,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                IsActive = p.IsActive,
                ProductIds = p.PromotionProducts?.Select(pp => pp.ProductId).ToList() ?? new List<Guid>(),
                Products = p.PromotionProducts?.Select(pp => new PromotionProductDto
                {
                    ProductId = pp.ProductId,
                    ProductName = pp.Product?.Name // Si tienes la relación cargada
                }).ToList() ?? new List<PromotionProductDto>()
            }).ToList();

            return result; // <-- Así devuelves el DTO enriquecido

        }

        public async Task<bool> UpdateAsync(PromotionDto dto, Guid entitiId)
        {
            var promotions = await _promotionRepository.GetByEntitiAsync(entitiId);
            var promotion = promotions.FirstOrDefault(p => p.PromotionId == dto.PromotionId);

            if (promotion == null)
                return false;

            // Actualiza los campos
            promotion.Name = dto.Name;
            promotion.Type = dto.Type;
            promotion.Percentage = dto.Percentage;
            promotion.MinQuantity = dto.MinQuantity;
            promotion.StartDate = dto.StartDate;
            promotion.EndDate = dto.EndDate;
            promotion.IsActive = dto.IsActive;

            // Actualiza productos asociados
            promotion.PromotionProducts = dto.ProductIds.Select(pid => new PromotionProduct
            {
                PromotionId = promotion.PromotionId,
                ProductId = pid
            }).ToList();

            await _promotionRepository.UpdateAsync(promotion);
            return true;
        }

        public async Task<string> ToggleStatusAsync(Guid id)
        {
            var promotion = await _promotionRepository
                .GetByEntitiAsync(
                    Guid.Parse(
                        _httpContextAccessor.HttpContext?
                        .User.FindFirst("entiti_id")?.Value!))
                .ContinueWith(t => t.Result
                    .FirstOrDefault(p => p.PromotionId == id));

            if (promotion == null)
                throw new Exception("Promoción no encontrada.");

            promotion.IsActive = !promotion.IsActive;

            await _promotionRepository.UpdateAsync(promotion);

            return promotion.IsActive
                ? "Promoción activada correctamente."
                : "Promoción desactivada correctamente.";
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var result = await _promotionRepository.DeleteAsync(id);

            if (!result)
                throw new Exception("No se pudo eliminar la promoción.");

            return "Promoción eliminada correctamente.";
        }
    }
}
