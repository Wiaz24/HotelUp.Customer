using System.Security.Claims;
using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Application.Commands;
using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Events.External;
using HotelUp.Customer.Shared.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelUp.Customer.API.Controllers;

[ApiController]
[Route("api/customer/commands")]
public class CommandsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IBus _bus;
    private Guid LoggedInUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) 
        is { } id ? new Guid(id) : throw new TokenException("No user id found in access token.");
    public CommandsController(ICommandDispatcher commandDispatcher, IBus bus)
    {
        _commandDispatcher = commandDispatcher;
        _bus = bus;
    }

    [HttpPost("create-reservation")]
    [Authorize]
    public async  Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
    {
        var command = new CreateReservation(
            LoggedInUserId, 
            dto.RoomNumbers, 
            dto.TenantsData, 
            dto.StartDate, 
            dto.EndDate);
        await _commandDispatcher.DispatchAsync(command);
        return Created();
    }
    
    [HttpPost("test-user-created-event")]
    public async Task<IActionResult> TestUserCreatedEvent()
    {
        await _bus.Publish(new UserCreatedEvent(Guid.NewGuid()));
        return Created();
    }
    
    [HttpPost("test-user-created-event/{id}")]
    public async Task<IActionResult> TestUserCreatedEvent(Guid id)
    {
        await _bus.Publish(new UserCreatedEvent(id));
        return Created();
    }
}