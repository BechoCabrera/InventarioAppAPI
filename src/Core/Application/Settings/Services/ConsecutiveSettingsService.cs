using InventarioBackend.src.Core.Domain.Settings.Interfaces;

namespace InventarioBackend.src.Core.Application.Settings.Services
{
    public class ConsecutiveSettingsService
    {
        private readonly IConsecutiveSettingsRepository _repository;

        public ConsecutiveSettingsService(IConsecutiveSettingsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene y genera el siguiente consecutivo para un nombre dado (ej. "ConsecutivoFactura")
        /// </summary>
        public async Task<string> GetNextConsecutiveAsync(string name)
        {
            var settings = await _repository.GetByNameAsync(name);
            if (settings == null)
                throw new Exception($"Consecutive settings not found for '{name}'");

            string nextNumber = settings.GenerateNextNumber();

            // Actualiza el último número usado
            settings.LastUsedNumber = settings.LastUsedNumber == 0 ? settings.StartNumber : settings.LastUsedNumber + settings.IncrementStep;
            settings.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(settings);

            return nextNumber;
        }
    }
}
