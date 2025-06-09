using InventarioBackend.src.Core.Application.EntitiConfigs.DTOs;
using InventarioBackend.src.Core.Application.Products.DTOs;

namespace InventarioBackend.src.Core.Application.EntitiConfigs.Interfaces
{
    public interface IEntitiConfigService
    {
        Task<EntitiConfigDto?> GetByCodeAsync(string code);
        Task<List<EntitiConfigDto>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
        Task<EntitiConfigDto> CreateAsync(EntitiConfigCreateDto dto);
        Task<EntitiConfigDto> UpdateAsync(EntitiConfigUpdateDto dto);
    }
}
