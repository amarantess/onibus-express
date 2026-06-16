using FluentValidation;
using OnibusExpress.Domain.Features.Trips.GetTripDetails;

namespace OnibusExpress.Application.Features.Trips.GetTripDetails;

public sealed class GetTripDetailsRequestValidator : AbstractValidator<GetTripDetailsRequest>
{
    public GetTripDetailsRequestValidator()
    {
        RuleFor(request => request.TripId)
            .NotEmpty();
    }
}
