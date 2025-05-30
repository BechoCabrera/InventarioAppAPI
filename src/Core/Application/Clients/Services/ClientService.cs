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

    public async Task<ClientDto?> GetByIdAsync(Guid id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        return client?.Adapt<ClientDto>();
    }

    public async Task AddAsync(ClientCreateDto dto)
    {
        var client = dto.Adapt<Client>();
        await _clientRepository.AddAsync(client);
    }

    public async Task UpdateAsync(Guid id, ClientUpdateDto dto)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null) return;

        dto.Adapt(client);
        client.UpdatedAt = DateTime.UtcNow;
        await _clientRepository.UpdateAsync(client);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _clientRepository.DeleteAsync(id);
    }
}
