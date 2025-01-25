using System.ComponentModel;
using System.Security.Claims;
using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Shared.Auth;
using HotelUp.Customer.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Customer.API.Controllers;

[ApiController]
[Route("api/customer/queries")]
[ProducesErrorResponseType(typeof(ErrorResponse))]
public class QueriesController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private Guid LoggedInUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) 
        is { } id ? new Guid(id) : throw new TokenException("No user id found in access token.");
    public QueriesController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet("get-free-rooms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation("Returns free rooms for provided query parameters")]
    public async Task<ActionResult<IEnumerable<RoomDto>>> Get([FromQuery] GetFreeRoomsDto dto)
    {
        var result = await _queryDispatcher
            .DispatchAsync(new GetFreeRooms(dto.StartDate, dto.EndDate, dto.RoomType, dto.RoomCapacity));
        return Ok(result);
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("get-users-reservations")]
    [SwaggerOperation("Returns all reservations of logged in user.")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
    {
        var query = new GetUsersReservations(LoggedInUserId);
        var result = await _queryDispatcher
            .DispatchAsync(query);
        return Ok(result);
    }

    [Authorize(Policy = PoliciesNames.CanManageReservations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("get-users-reservations/{userId:guid}")]
    [SwaggerOperation("Returns all reservations of provided user. Requires to be in role: Admins/Receptionists")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations(Guid userId)
    {
        var query = new GetUsersReservations(userId);
        var result = await _queryDispatcher
            .DispatchAsync(query);
        return Ok(result);
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    
    [Authorize]
    [HttpGet("logged-in-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetLoggedInUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok($"Hello {email}!");
    }
}