using System.ComponentModel;
using System.Security.Claims;
using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
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
    
    [HttpGet("free-rooms")]
    public async Task<ActionResult<IEnumerable<RoomDto>>> Get([FromQuery] GetFreeRoomsDto dto)
    {
        var result = await _queryDispatcher
            .DispatchAsync(new GetFreeRooms(dto.StartDate, dto.EndDate));
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOne()
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