using InventarioBackend.src.Core.Domain.Clients.Entities;

namespace InventarioBackend.src.Core.Domain.Clients.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(Guid id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Guid id);
    }
}
