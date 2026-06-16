using OnibusExpress.Domain.Features.Trips.SearchTrips;

namespace OnibusExpress.Application.Features.Trips.SearchTrips;

public interface ISearchTripsUseCase
{
    Task<SearchTripsResponse> ExecuteAsync(SearchTripsRequest request, CancellationToken cancellationToken = default);
}
