using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Repositories;

namespace HotelUp.Customer.Application.Commands.Handlers;

public class CreateRoomHandler : ICommandHandler<CreateRoom>
{
    private readonly IRoomFactory _roomFactory;
    private readonly IRoomRepository _roomRepository;

    public CreateRoomHandler(IRoomRepository roomRepository, IRoomFactory roomFactory)
    {
        _roomRepository = roomRepository;
        _roomFactory = roomFactory;
    }

    public async Task HandleAsync(CreateRoom command)
    {
        var room = await _roomFactory.Create(
            command.Number, command.Capacity, command.Floor, 
            command.WithSpecialNeeds, command.Type, command.ImageUrl);
        await _roomRepository.AddAsync(room);
    }
}