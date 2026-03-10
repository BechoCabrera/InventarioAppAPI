using InventarioBackend.src.Core.Domain.Security.Entities;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace InventarioBackend.src.Infrastructure.Data.Repositories.Security
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserRole>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.UserRoles
                .Include(ur => ur.Role) // para poder leer RoleName en el servicio
                .Where(ur => ur.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public Task RemoveRangeAsync(
            IEnumerable<UserRole> entities,
            CancellationToken cancellationToken = default)
        {
            _context.UserRoles.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task AddRangeAsync(
            IEnumerable<UserRole> entities,
            CancellationToken cancellationToken = default)
        {
            await _context.UserRoles.AddRangeAsync(entities, cancellationToken);
        }

        public Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
