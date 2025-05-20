using InventarioBackend.src.Core.Domain.Security.Entities;
using System.Threading.Tasks;

namespace InventarioBackend.src.Core.Domain.Security.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int userId);
    }
}
