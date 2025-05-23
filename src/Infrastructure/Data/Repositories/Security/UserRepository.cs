using InventarioBackend.src.Core.Domain.Security.Entities;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InventarioBackend.src.Infrastructure.Data.Repositories.Security
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetByUsernameAsync(string username, string pass)
        {
            User? result = new User();
            try
            {
                result = await _context.Users
               .Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ThenInclude(r => r.RolePermissions) // Opcional, si usas permisos más adelante
               .Include(u => u.UserPermissions).ThenInclude(up => up.Permission)   // Opcional, si accedes a permisos directos
               .FirstOrDefaultAsync(u => u.Username == username && u.ValidatePassword(u.PasswordHash).ToString() == pass);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
