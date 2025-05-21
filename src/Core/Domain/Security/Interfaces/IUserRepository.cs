using InventarioBackend.src.Core.Domain.Security.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioBackend.src.Core.Domain.Security.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);

    }
}
