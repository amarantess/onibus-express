using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Features.Trips.GetTripDetails;
using OnibusExpress.Domain.Features.Trips.SearchTrips;

namespace OnibusExpress.Domain.Repositories;

public interface ITripRepository
{
    Task<IReadOnlyCollection<TripSummaryResponse>> SearchAsync(SearchTripsRequest request, CancellationToken cancellationToken = default);
    Task<TripDetailsResponse?> GetDetailsAsync(Guid tripId, CancellationToken cancellationToken = default);
    Task<Trip?> GetByIdAsync(Guid tripId, CancellationToken cancellationToken = default);
}
