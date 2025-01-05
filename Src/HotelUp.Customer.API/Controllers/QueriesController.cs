using System.ComponentModel;
using System.Security.Claims;
using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Customer.API.Controllers;

[ApiController]
[Route("api/customer/queries")]
public class QueriesController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private Guid LoggedInUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) 
        is { } id ? new Guid(id) : throw new TokenException("No user id found in access token.");
    public QueriesController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet("free-rooms")]
    [ProducesResponseType(200)]
    [SwaggerOperation("Returns free rooms for provided query parameters")]
    public async Task<ActionResult<IEnumerable<RoomDto>>> Get([FromQuery] GetFreeRoomsDto dto)
    {
        var result = await _queryDispatcher
            .DispatchAsync(new GetFreeRooms(dto.StartDate, dto.EndDate, dto.RoomType, dto.RoomCapacity));
        return Ok(result);
    }
    
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [HttpGet("get-users-reservations")]
    [SwaggerOperation("Returns all reservations of the logged in user")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
    {
        var result = await _queryDispatcher
            .DispatchAsync(new GetUsersReservations(LoggedInUserId));
        return Ok(result);
    }
    
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [HttpGet("get-users-reservation/{id}")]
    [SwaggerOperation("Returns reservation by id")]
    public async Task<ActionResult<ReservationDto>> GetReservationById([FromRoute] Guid id)
    {
        var query = new GetReservationById(LoggedInUserId, id);
        var result = await _queryDispatcher
            .DispatchAsync(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet]
    [SwaggerIgnore]
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