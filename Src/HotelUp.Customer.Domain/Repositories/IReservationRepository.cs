using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Domain.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetAsync(Guid id);
    Task AddAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(Reservation reservation);
}