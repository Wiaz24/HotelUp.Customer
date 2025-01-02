using System.Security.Claims;
using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Application.Commands;
using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Events.External;
using HotelUp.Customer.Shared.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Customer.API.Controllers;

[ApiController]
[Route("api/customer/commands")]
public class CommandsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IBus _bus;
    private readonly ILogger<CommandsController> _logger;
    private Guid LoggedInUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) 
        is { } id ? new Guid(id) : throw new TokenException("No user id found in access token.");
    public CommandsController(ICommandDispatcher commandDispatcher, IBus bus, ILogger<CommandsController> logger)
    {
        _commandDispatcher = commandDispatcher;
        _bus = bus;
        _logger = logger;
    }
    
    [Authorize]
    [HttpPost("create-reservation")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [SwaggerOperation("Create new reservation")]
    public async  Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
    {
        var command = new CreateReservation(
            LoggedInUserId, 
            dto.RoomNumbers, 
            dto.TenantsData.Select(dto => dto.ToTenantData()), 
            dto.StartDate, 
            dto.EndDate);
        await _commandDispatcher.DispatchAsync(command);
        return Created("Reservation created successfully", null);
    }
    
    [Authorize]
    [HttpPost("create-room")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [SwaggerOperation("Create new room")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto dto)
    {
        var command = dto.ToCreateRoom();
        await _commandDispatcher.DispatchAsync(command);
        return Created();
    }
    
    [Authorize]
    [HttpPost("create-client")]
    [Obsolete]
    [SwaggerOperation("ONLY FOR TESTING! Create new client")]
    public async Task<IActionResult> TestUserCreatedEvent()
    {
        var id = LoggedInUserId;
        _logger.LogWarning("Creating user with id: {Id}", id);
        await _bus.Publish(new UserCreatedEvent(id));
        return Created();
    }
}