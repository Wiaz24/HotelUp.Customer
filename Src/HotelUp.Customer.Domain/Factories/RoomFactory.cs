using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Factories.Exceptions;
using HotelUp.Customer.Domain.Repositories;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Factories;

public class RoomFactory : IRoomFactory
{
    private readonly IRoomRepository _roomRepository;

    public RoomFactory(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Room> Create(int number, RoomCapacity capacity, RoomFloor floor, 
        bool withSpecialNeeds, RoomType type, ImageUrl imageUrl)
    {
        var existingRoom = await _roomRepository.GetRoomAsync(number);
        if (existingRoom is not null)
        {
            throw new RoomAlreadyExistsException(number);
        }
        return new Room(number, capacity, floor, withSpecialNeeds, type, imageUrl);
    }
}