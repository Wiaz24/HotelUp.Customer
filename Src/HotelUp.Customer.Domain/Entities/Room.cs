using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Room : AggregateRoot<int>
{
    public int Number => Id;
    public RoomCapacity Capacity { get; private set; } = null!;
    public RoomFloor Floor { get; private set; } = null!;
    public bool WithSpecialNeeds { get; private set; }
    public RoomType Type { get; private set; }
    public ImageUrl ImageUrl { get; private set; } = null!;

    internal Room(int number, RoomCapacity capacity, RoomFloor floor, 
        bool withSpecialNeeds, RoomType type, ImageUrl imageUrl)
    {
        Id = number;
        Capacity = capacity;
        Floor = floor;
        WithSpecialNeeds = withSpecialNeeds;
        Type = type;
        ImageUrl = imageUrl;
    }
    
    private Room()
    {
    }
}