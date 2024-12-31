using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Factories;

public interface IRoomFactory
{
    Task<Room> Create(int number, RoomCapacity capacity, 
        RoomFloor floor, bool withSpecialNeeds, RoomType type, ImageUrl imageUrl);
}