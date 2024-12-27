using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using HotelUp.Customer.Infrastructure.EF.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelUp.Customer.API.Controllers;

[ApiController]
[Route("api/Customer/queries")]
public class QueriesController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;

    public QueriesController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetOne()
    {
        var result = await _queryDispatcher.DispatchAsync(new TestDatabase());
        return Ok(result);
    }
}