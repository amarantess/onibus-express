using Microsoft.AspNetCore.Mvc;
using OnibusExpress.Application.Features.Trips.GetTripDetails;
using OnibusExpress.Application.Features.Trips.SearchTrips;
using OnibusExpress.Domain.Features.Trips.GetTripDetails;
using OnibusExpress.Domain.Features.Trips.SearchTrips;

namespace OnibusExpress.Api.Controllers;

[ApiController]
[Route("viagens")]
public class TripsController : ControllerBase
{
    private readonly ISearchTripsUseCase _searchTripsUseCase;
    private readonly IGetTripDetailsUseCase _getTripDetailsUseCase;

    public TripsController(ISearchTripsUseCase searchTripsUseCase, IGetTripDetailsUseCase getTripDetailsUseCase)
    {
        _searchTripsUseCase = searchTripsUseCase;
        _getTripDetailsUseCase = getTripDetailsUseCase;
    }

    [HttpGet]
    [ProducesResponseType<SearchTripsResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchTrips([FromQuery] string origin, [FromQuery] string destination, [FromQuery] DateOnly travelDate, CancellationToken cancellationToken)
    {
		SearchTripsRequest request = new()
		{
            Origin = origin,
            Destination = destination,
            TravelDate = travelDate
        };

        var response = await _searchTripsUseCase.ExecuteAsync(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<TripDetailsResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTripDetails([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _getTripDetailsUseCase.ExecuteAsync(id, cancellationToken);
        return Ok(response);
    }
}
