using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.Application.Commands;

public record CreateRoom(
    int Number,
    int Capacity,
    int Floor,
    bool WithSpecialNeeds,
    RoomType Type,
    string ImageUrl) : ICommand;