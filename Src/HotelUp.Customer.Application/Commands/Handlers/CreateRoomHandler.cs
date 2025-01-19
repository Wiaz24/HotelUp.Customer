using HotelUp.Customer.Application.ApplicationServices;
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
    private readonly IRoomImageService _roomImageService;

    public CreateRoomHandler(IRoomRepository roomRepository, IRoomFactory roomFactory, 
        IPublishEndpoint bus, IRoomImageService roomImageService)
    {
        _roomRepository = roomRepository;
        _roomFactory = roomFactory;
        _bus = bus;
        _roomImageService = roomImageService;
    }

    public async Task HandleAsync(CreateRoom command)
    {
        var exampleUri = "https://example.com/image.jpg";

        var room = await _roomFactory.Create(
            command.Number, command.Capacity, command.Floor, 
            command.WithSpecialNeeds, command.Type, exampleUri);
        
        var imageUri = await _roomImageService.UploadImageAsync(command.Number, command.Image);
        room.SetImageUri(imageUri);
        await _roomRepository.AddAsync(room);
        await _bus.Publish(new RoomCreatedEvent(new RoomDto(room)));
    }
}