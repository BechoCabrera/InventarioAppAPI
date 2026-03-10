using InventarioBackend.src.Core.Domain.Settings.Entities;
using InventarioBackend.src.Core.Domain.Settings.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Infrastructure.Data.Repositories.Settings
{
    public class ConsecutiveSettingsRepository : IConsecutiveSettingsRepository
    {
        private readonly AppDbContext _context;

        public ConsecutiveSettingsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ConsecutiveSettings?> GetByNameAsync()
        {
            try
            {
                var result = await _context.Set<ConsecutiveSettings>().FirstOrDefaultAsync(c => c.YearPrefix == DateTime.Now.Year.ToString());
                if (result == null)
                {
                    ConsecutiveSettings? yearAnterior = await _context.Set<ConsecutiveSettings>().FirstOrDefaultAsync(c => c.YearPrefix == (DateTime.Now.Year - 1).ToString());
                    if (yearAnterior != null)
                    {
                        yearAnterior.YearPrefix = DateTime.Now.Year.ToString();
                        yearAnterior.LastUsedNumber = 0;
                        yearAnterior.Id = Guid.NewGuid();
                        _context.Set<ConsecutiveSettings>().AddAsync(yearAnterior);
                        await _context.SaveChangesAsync();
                        return yearAnterior;
                    }
                    else
                    {
                        throw new Exception("Error de consecutivo para el año actual.");
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task UpdateAsync(ConsecutiveSettings settings)
        {
            _context.Set<ConsecutiveSettings>().Update(settings);
            await _context.SaveChangesAsync();
        }
    }
}
