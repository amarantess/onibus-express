using FluentValidation;
using OnibusExpress.Domain.Features.Reservations.CancelReservation;

namespace OnibusExpress.Application.Features.Reservations.CancelReservation;

public sealed class CancelReservationRequestValidator : AbstractValidator<CancelReservationRequest>
{
    public CancelReservationRequestValidator()
    {
        RuleFor(request => request.Code)
            .NotEmpty()
            .MaximumLength(16);
    }
}
