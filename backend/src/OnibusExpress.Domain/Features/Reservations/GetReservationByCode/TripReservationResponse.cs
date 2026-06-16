namespace OnibusExpress.Domain.Features.Reservations.GetReservationByCode;

public sealed class TripReservationResponse
{
    public Guid TripId { get; init; }
    public Guid RouteId { get; init; }
    public string Origin { get; init; } = string.Empty;
    public string Destination { get; init; } = string.Empty;
    public DateTimeOffset DepartureAt { get; init; }
    public decimal BasePrice { get; init; }
}
