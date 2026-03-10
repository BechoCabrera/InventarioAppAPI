using InventarioBackend.src.Core.Domain.Security.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioBackend.src.Core.Domain.Security.Interfaces
{
    public interface IUserRepository
    {
        // Métodos que ya usas en otras partes
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username, string pass);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);

        // Nuevos métodos para listado/creación usados por UserService (pantallas nuevas)
        Task<List<User>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task AddAsync(
            User user,
            CancellationToken cancellationToken);   // sobrecarga con token

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
