using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Entities.Exceptions;

public class ReservationCannotBeCanceledException : BusinessRuleException
{
    public ReservationCannotBeCanceledException() 
        : base("Reservation can be canceled only 24 hours before the start.")
    {
    }
}