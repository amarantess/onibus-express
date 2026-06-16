namespace OnibusExpress.Domain.Features.Trips.GetTripDetails;

public sealed class TripDetailsResponse
{
    public Guid TripId { get; init; }
    public Guid RouteId { get; init; }
    public string Origin { get; init; } = string.Empty;
    public string Destination { get; init; } = string.Empty;
    public TimeSpan EstimatedDuration { get; init; }
    public DateTimeOffset DepartureAt { get; init; }
    public decimal BasePrice { get; init; }
    public int AvailableSeats { get; init; }
    public IReadOnlyCollection<SeatAvailabilityResponse> Seats { get; init; } = [];
}
