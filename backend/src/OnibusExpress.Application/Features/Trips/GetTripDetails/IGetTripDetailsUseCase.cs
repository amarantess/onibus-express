using OnibusExpress.Domain.Features.Trips.GetTripDetails;

namespace OnibusExpress.Application.Features.Trips.GetTripDetails;

public interface IGetTripDetailsUseCase
{
    Task<TripDetailsResponse> ExecuteAsync(Guid tripId, CancellationToken cancellationToken = default);
}
