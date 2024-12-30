using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Room : AggregateRoot<int>
{
    public int Number => Id;
    public RoomCapacity Capacity { get; private set; }
    public RoomFloor Floor { get; private set; }
    public bool WithSpecialNeeds { get; private set; }
    public RoomType Type { get; private set; }
    public ImageUrl ImageUrl { get; private set; }
    
}