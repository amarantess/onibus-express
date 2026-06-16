using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Domain.Features.Reservations.CancelReservation;

public sealed class CancelReservationResponse
{
    public string Code { get; init; } = string.Empty;
    public ReservationStatus Status { get; init; }
    public DateTimeOffset CancelledAt { get; init; }
}
