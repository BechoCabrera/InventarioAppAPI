using InventarioBackend.src.Core.Domain.Security.Entities;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Infrastructure.Data.Repositories.Security
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
              .Include(r => r.RolePermissions)
              .Include(r => r.UserRoles) // Incluye los usuarios asignados al rol
              .FirstOrDefaultAsync(r => r.RoleId == id, cancellationToken);
        }

        public async Task AddAsync(
            Role entity,
            CancellationToken cancellationToken = default)
        {
            await _context.Roles.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(Role entity, CancellationToken cancellationToken = default)
        {
            _context.Roles.Update(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Role entity, CancellationToken cancellationToken = default)
        {
            _context.Roles.Remove(entity);
            await Task.CompletedTask;
        }

        public Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
