using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Events;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Repositories;

using MassTransit;

namespace HotelUp.Customer.Application.Commands.Handlers;

public class CreateRoomHandler : ICommandHandler<CreateRoom>
{
    private readonly IRoomFactory _roomFactory;
    private readonly IRoomRepository _roomRepository;
    private readonly IPublishEndpoint _bus;

    public CreateRoomHandler(IRoomRepository roomRepository, IRoomFactory roomFactory, IPublishEndpoint bus)
    {
        _roomRepository = roomRepository;
        _roomFactory = roomFactory;
        _bus = bus;
    }

    public async Task HandleAsync(CreateRoom command)
    {
        var room = await _roomFactory.Create(
            command.Number, command.Capacity, command.Floor, 
            command.WithSpecialNeeds, command.Type, command.ImageUrl);
        await _roomRepository.AddAsync(room);
        await _bus.Publish(new RoomCreatedEvent(new RoomDto(room)));
    }
}