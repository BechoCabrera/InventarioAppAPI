using InventarioBackend.src.Core.Application.Promotions.DTOs;
using InventarioBackend.src.Core.Domain.Promotions.Entities;

namespace InventarioBackend.src.Core.Application.Promotions.Interfaces
{
    public interface IPromotionService
    {
        Task<bool> UpdateAsync(PromotionDto dto, Guid entitiId);
        Task<Guid> CreateAsync(PromotionCreateDto dto);
        Task<List<PromotionDto>> GetByEntitiAsync(Guid entitiId);
        Task<CalculatePromotionResponseDto> CalculateAsync(List<CartItemDto> items, Guid entitiId);
        Task<string> ToggleStatusAsync(Guid id);
        Task<string> DeleteAsync(Guid id);
    }
}
