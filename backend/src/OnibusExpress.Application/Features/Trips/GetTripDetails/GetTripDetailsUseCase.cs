using OnibusExpress.Domain.Features.Trips.GetTripDetails;
using OnibusExpress.Domain.Repositories;

namespace OnibusExpress.Application.Features.Trips.GetTripDetails;

public sealed class GetTripDetailsUseCase : IGetTripDetailsUseCase
{
    private readonly ITripRepository _tripRepository;

    public GetTripDetailsUseCase(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<TripDetailsResponse> ExecuteAsync(Guid tripId, CancellationToken cancellationToken = default)
    {
        if (tripId == Guid.Empty)
			throw new Exception("Trip id is invalid.");

        var tripDetails = await _tripRepository.GetDetailsAsync(tripId, cancellationToken);
        if (tripDetails is null)
			throw new Exception("Trip was not found.");

		return tripDetails;
    }
}
