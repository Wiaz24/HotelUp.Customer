using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Domain.Consts;

using Microsoft.AspNetCore.Http;

namespace HotelUp.Customer.Application.Commands;

public record CreateRoom(
    int Number,
    int Capacity,
    int Floor,
    bool WithSpecialNeeds,
    RoomType Type,
    IFormFile Image) : ICommand;