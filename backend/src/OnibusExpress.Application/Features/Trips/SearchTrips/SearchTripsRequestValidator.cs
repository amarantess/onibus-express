using FluentValidation;
using OnibusExpress.Domain.Features.Trips.SearchTrips;

namespace OnibusExpress.Application.Features.Trips.SearchTrips;

public sealed class SearchTripsRequestValidator : AbstractValidator<SearchTripsRequest>
{
    public SearchTripsRequestValidator()
    {
        RuleFor(request => request.Origin)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(request => request.Destination)
            .NotEmpty()
            .MaximumLength(120);

        RuleFor(request => request.TravelDate)
            .NotEqual(default(DateOnly));
    }
}
