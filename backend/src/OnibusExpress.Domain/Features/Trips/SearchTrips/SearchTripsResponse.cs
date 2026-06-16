namespace OnibusExpress.Domain.Features.Trips.SearchTrips;

public sealed class SearchTripsResponse
{
    public IReadOnlyCollection<TripSummaryResponse> Trips { get; init; } = [];
}
