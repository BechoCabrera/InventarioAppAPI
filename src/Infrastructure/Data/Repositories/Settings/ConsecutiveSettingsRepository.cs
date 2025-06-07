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

        public async Task<ConsecutiveSettings?> GetByNameAsync(string name)
        {
            try
            {
                var result = await _context.Set<ConsecutiveSettings>().FirstOrDefaultAsync(c => c.Name == name);
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
