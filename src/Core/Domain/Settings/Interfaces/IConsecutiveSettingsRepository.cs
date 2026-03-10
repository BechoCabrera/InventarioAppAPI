using InventarioBackend.src.Core.Domain.Settings.Entities;

namespace InventarioBackend.src.Core.Domain.Settings.Interfaces
{
    public interface IConsecutiveSettingsRepository
    {
        Task<ConsecutiveSettings?> GetByNameAsync();
        Task UpdateAsync(ConsecutiveSettings settings);
    }
}
