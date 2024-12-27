using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Queries.Handlers;

public class TestDatabaseHandler : IQueryHandler<TestDatabase, int>
{
    private readonly ReadDbContext _context;

    public TestDatabaseHandler(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<int> HandleAsync(TestDatabase query)
    {
        var result = await _context.Database.ExecuteSqlRawAsync("SELECT 1");
        return result;
    }
}