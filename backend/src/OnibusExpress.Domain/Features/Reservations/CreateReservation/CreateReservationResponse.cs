using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Domain.Features.Reservations.CreateReservation;

public sealed class CreateReservationResponse
{
    public string Code { get; init; } = string.Empty;
    public Guid TripId { get; init; }
    public Guid PassengerId { get; init; }
    public int SeatNumber { get; init; }
    public ReservationStatus Status { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}
