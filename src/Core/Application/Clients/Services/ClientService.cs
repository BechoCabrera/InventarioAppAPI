using InventarioBackend.src.Core.Application.Clients.DTOs;
using InventarioBackend.src.Core.Application.Clients.Interfaces;
using InventarioBackend.src.Core.Domain.Clients.Entities;
using InventarioBackend.src.Core.Domain.Clients.Interfaces;
using Mapster;

namespace InventarioBackend.Core.Application.Clients.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<List<ClientDto>> GetAllAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        return clients.Adapt<List<ClientDto>>();
    }
    public async Task<List<ClientDto>> GetByEntitiAsync(Guid id)
    {
        var clients = await _clientRepository.GetByEntitiAsync(id);
        return clients.Adapt<List<ClientDto>>();
    }

    public async Task<ClientDto?> GetByIdAsync(Guid id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        return client?.Adapt<ClientDto>();
    }

    public async Task AddAsync(ClientCreateDto dto)
    {
        var client = dto.Adapt<Client>();
        client.Name = client.Name.ToUpper();
        await _clientRepository.AddAsync(client);
    }

    public async Task<bool> UpdateAsync(Guid id, ClientUpdateDto dto)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null) return false;
        client.Name = dto.Name;
        client.Nit = dto.Nit;
        client.Email = dto.Email;
        client.Phone = dto.Phone;
        client.UpdatedAt = DateTime.UtcNow;

        var result = await _clientRepository.UpdateAsync(client);
        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _clientRepository.DeleteAsync(id);
    }
}
