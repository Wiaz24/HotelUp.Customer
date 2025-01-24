using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Repositories;
using HotelUp.Customer.Infrastructure.EFCore.Contexts;

using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Repositories;

public class PostgresClientRepository : IClientRepository
{
    private readonly DbSet<Client> _clients;
    private readonly WriteDbContext _dbContext;

    public PostgresClientRepository(WriteDbContext dbContext)
    {
        _clients = dbContext.Set<Client>();
        _dbContext = dbContext;
    }

    public Task<Client?> GetAsync(Guid id)
    {
        return _clients.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Client client)
    {
        await _clients.AddAsync(client);
        await _dbContext.SaveChangesAsync();
    }
}