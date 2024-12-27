using HotelUp.Customer.Application.Commands.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace HotelUp.Customer.API.Controllers;

[ApiController]
[Route("api/Customer/commands")]
public class CommandsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CommandsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public IActionResult Create()
    {
        return Created();
    }
}