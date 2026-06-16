using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Domain.Features.Reservations.GetReservationByCode;

public sealed class GetReservationByCodeResponse
{
    public string Code { get; init; } = string.Empty;
    public ReservationStatus Status { get; init; }
    public int SeatNumber { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? CancelledAt { get; init; }
    public PassengerReservationResponse Passenger { get; init; } = new();
    public TripReservationResponse Trip { get; init; } = new();
}
