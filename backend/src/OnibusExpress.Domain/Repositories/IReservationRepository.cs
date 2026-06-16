using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Features.Reservations.Shared;

namespace OnibusExpress.Domain.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<ReservationDetailsDto?> GetDetailsByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<bool> ReservationCodeExistsAsync(string code, CancellationToken cancellationToken = default);
    Task<bool> SeatIsOccupiedAsync(Guid tripId, int seatNumber, CancellationToken cancellationToken = default);
    Task CreateAsync(Reservation reservation, CancellationToken cancellationToken = default);
    void Update(Reservation reservation);
}
