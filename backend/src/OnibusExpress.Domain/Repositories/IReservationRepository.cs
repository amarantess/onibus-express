using OnibusExpress.Domain.Features.Reservations.Shared;

namespace OnibusExpress.Domain.Repositories;

public interface IReservationRepository
{
    Task<ReservationRecordDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<bool> ReservationCodeExistsAsync(string code, CancellationToken cancellationToken = default);
    Task<bool> SeatIsOccupiedAsync(Guid tripId, int seatNumber, CancellationToken cancellationToken = default);
    Task<ReservationRecordDto> CreateAsync(ReservationWriteDto reservation, CancellationToken cancellationToken = default);
    Task<ReservationRecordDto> CancelAsync(string code, DateTimeOffset cancelledAt, CancellationToken cancellationToken = default);
}
