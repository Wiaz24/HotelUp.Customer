using System.Security.Claims;
using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Application.ApplicationServices;
using HotelUp.Customer.Application.Commands;
using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Events.External;
using HotelUp.Customer.Shared.Auth;
using HotelUp.Customer.Shared.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Customer.API.Controllers;

[ApiController]
[Route("api/customer/commands")]
[ProducesErrorResponseType(typeof(ErrorResponse))]
public class CommandsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IBus _bus;
    private readonly IReservationOwnershipService _reservationOwnershipService; 
    private Guid LoggedInUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) 
        is { } id ? new Guid(id) : throw new TokenException("No user id found in access token.");
    public CommandsController(ICommandDispatcher commandDispatcher, IBus bus, 
        IReservationOwnershipService reservationOwnershipService)
    {
        _commandDispatcher = commandDispatcher;
        _bus = bus;
        _reservationOwnershipService = reservationOwnershipService;
    }
    
    [Authorize(Policy = PoliciesNames.CanManageReservations)]
    [HttpPost("create-reservation")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Create new reservation")]
    public async  Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
    {
        var command = new CreateReservation(
            LoggedInUserId, 
            dto.RoomNumbers, 
            dto.TenantsData.Select(td => td.ToTenantData()), 
            dto.StartDate, 
            dto.EndDate);
        var id = await _commandDispatcher.DispatchAsync(command);
        return Created($"api/customer/queries/get-users-reservation/{id}", id);
    }
    
    [Authorize(Policy = PoliciesNames.CanManageReservations)]
    [HttpPost("cancel-reservation/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation("Cancel reservation by id")]
    public async Task<IActionResult> CancelReservation([FromRoute] Guid id)
    {
        if (!await _reservationOwnershipService.IsReservationOwner(id, LoggedInUserId))
        {
            return Unauthorized();
        }
        var command = new CancelReservation(id);
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }
    
    [Authorize(Policy = PoliciesNames.IsAdmin)]
    [HttpPost("create-room")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Create new room")]
    public async Task<IActionResult> CreateRoom([FromForm] CreateRoomDto dto)
    {
        var command = new CreateRoom(dto.Number, dto.Capacity, dto.Floor, dto.WithSpecialNeeds, dto.Type, dto.Image);
        await _commandDispatcher.DispatchAsync(command);
        return Created("",LoggedInUserId);
    }
    
    [Obsolete]
    [Authorize]
    [HttpPost("create-client")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("ONLY FOR TESTING! Create new client")]
    public async Task<IActionResult> TestUserCreatedEvent()
    {
        var id = LoggedInUserId;
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email is null)
        {
            return Unauthorized();
        }
        await _bus.Publish(new UserCreatedEvent(id, email));
        return Created("", id);
    }
    
    [HttpPost("publish-user-created-event")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Publish user created event. Only for AWS Lambda. Requires API key.")]
    public async Task<IActionResult> PublishUserCreatedEvent([FromBody] UserCreatedDto dto)
    {
        var command = new PublishUserCreated(dto.ApiKey, dto.UserId, dto.UserEmail);
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}