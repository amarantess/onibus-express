namespace OnibusExpress.Domain.Features.Reservations.CancelReservation;

public sealed class CancelReservationRequest
{
    public string Code { get; init; } = string.Empty;
}
