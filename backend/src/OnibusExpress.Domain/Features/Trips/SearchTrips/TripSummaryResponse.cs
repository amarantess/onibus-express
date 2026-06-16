namespace OnibusExpress.Domain.Features.Trips.SearchTrips;

public sealed class TripSummaryResponse
{
    public Guid TripId { get; init; }
    public Guid RouteId { get; init; }
    public string Origin { get; init; } = string.Empty;
    public string Destination { get; init; } = string.Empty;
    public DateTimeOffset DepartureAt { get; init; }
    public decimal BasePrice { get; init; }
    public int AvailableSeats { get; init; }
    public int RemainingSeats { get; init; }
}
