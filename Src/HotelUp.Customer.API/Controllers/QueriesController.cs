using System.Security.Claims;
using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelUp.Customer.API.Controllers;

[ApiController]
[Route("api/customer/queries")]
public class QueriesController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    public QueriesController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet]
    public async  Task<IActionResult> GetOne()
    {
        var result = await _queryDispatcher.DispatchAsync(new TestDatabase());
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("logged-in-user")]
    public IActionResult GetLoggedInUser()
    {
        var userName = User.FindFirstValue(ClaimTypes.Email);
        return Ok($"Hello {userName}!");
    }
}