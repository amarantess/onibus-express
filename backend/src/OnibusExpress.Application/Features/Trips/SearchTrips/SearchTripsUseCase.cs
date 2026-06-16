using FluentValidation;
using OnibusExpress.Domain.Features.Trips.SearchTrips;
using OnibusExpress.Domain.Repositories;

namespace OnibusExpress.Application.Features.Trips.SearchTrips;

public sealed class SearchTripsUseCase : ISearchTripsUseCase
{
    private readonly IValidator<SearchTripsRequest> _validator;
    private readonly ITripRepository _tripRepository;

    public SearchTripsUseCase(IValidator<SearchTripsRequest> validator, ITripRepository tripRepository)
    {
        _validator = validator;
        _tripRepository = tripRepository;
    }

    public async Task<SearchTripsResponse> ExecuteAsync(SearchTripsRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var trips = await _tripRepository.SearchAsync(request, cancellationToken);

        return new SearchTripsResponse
        {
            Trips = trips
        };
    }
}
