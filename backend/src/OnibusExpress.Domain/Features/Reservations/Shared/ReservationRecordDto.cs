using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Domain.Features.Reservations.Shared;

public sealed class ReservationRecordDto
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public Guid TripId { get; init; }
    public Guid PassengerId { get; init; }
    public int SeatNumber { get; init; }
    public ReservationStatus Status { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? CancelledAt { get; init; }
}
