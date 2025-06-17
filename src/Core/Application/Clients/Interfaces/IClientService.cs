using InventarioBackend.src.Core.Application.Clients.DTOs;

namespace InventarioBackend.src.Core.Application.Clients.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAllAsync();
        Task<List<ClientDto>> GetByEntitiAsync(Guid id);
        Task<ClientDto?> GetByIdAsync(Guid id);
        Task AddAsync(ClientCreateDto dto);
        Task<bool> UpdateAsync(Guid id, ClientUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
