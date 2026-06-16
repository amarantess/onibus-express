namespace OnibusExpress.Domain.Features.Trips.SearchTrips;

public sealed class SearchTripsRequest
{
    public string Origin { get; init; } = string.Empty;
    public string Destination { get; init; } = string.Empty;
    public DateOnly TravelDate { get; init; }
}
