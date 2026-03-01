using InventarioBackend.src.Core.Application.Promotions.DTOs;

namespace InventarioBackend.src.Core.Application.Promotions.Interfaces
{
    public interface IPromotionService
    {
        Task<Guid> CreateAsync(PromotionCreateDto dto);
        Task<List<object>> GetByEntitiAsync(Guid entitiId);
        Task<CalculatePromotionResponseDto> CalculateAsync(List<CartItemDto> items, Guid entitiId);
        Task<string> ToggleStatusAsync(Guid id);
        Task<string> DeleteAsync(Guid id);
    }
}
