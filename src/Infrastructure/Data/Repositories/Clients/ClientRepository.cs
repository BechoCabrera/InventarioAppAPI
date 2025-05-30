using InventarioBackend.src.Core.Domain.Clients.Entities;
using InventarioBackend.src.Core.Domain.Clients.Interfaces;
using InventarioBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.Infrastructure.Data.Repositories.Clients;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Client> _dbSet;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Client>();
    }

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<Client>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(Client client)
    {
        await _dbSet.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        _dbSet.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var client = await _dbSet.FindAsync(id);
        if (client != null)
        {
            _dbSet.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
