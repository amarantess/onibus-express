using OnibusExpress.Domain.Features.Trips.GetTripDetails;
using OnibusExpress.Domain.Repositories;
using OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;

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
			throw new ErrorOnValidationException("Trip id is invalid.");

        var tripDetails = await _tripRepository.GetDetailsAsync(tripId, cancellationToken);
        if (tripDetails is null)
			throw new NotFoundException("Trip was not found.");

		return tripDetails;
    }
}
