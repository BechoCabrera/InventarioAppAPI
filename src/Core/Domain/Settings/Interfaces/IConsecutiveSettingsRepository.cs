using InventarioBackend.src.Core.Domain.Settings.Entities;

namespace InventarioBackend.src.Core.Domain.Settings.Interfaces
{
    public interface IConsecutiveSettingsRepository
    {
        Task<ConsecutiveSettings?> GetByNameAsync(string name);
        Task UpdateAsync(ConsecutiveSettings settings);
    }
}
