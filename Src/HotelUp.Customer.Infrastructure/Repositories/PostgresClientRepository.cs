using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Repositories;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Repositories;

public class PostgresClientRepository : IClientRepository
{
    private readonly DbSet<Client> _clients;

    public PostgresClientRepository(WriteDbContext dbContext)
    {
        _clients = dbContext.Set<Client>();
    }

    public Task<Client?> GetAsync(Guid id)
    {
        return _clients.FirstOrDefaultAsync(c => c.Id == id);
    }
}