using HotelUp.Customer.Application.Commands.Abstractions;

namespace HotelUp.Customer.Application.Commands;

public record CancelReservation(Guid Id) : ICommand;