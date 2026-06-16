using FluentValidation;
using OnibusExpress.Domain.Features.Reservations.GetReservationByCode;

namespace OnibusExpress.Application.Features.Reservations.GetReservationByCode;

public sealed class GetReservationByCodeRequestValidator : AbstractValidator<GetReservationByCodeRequest>
{
    public GetReservationByCodeRequestValidator()
    {
        RuleFor(request => request.Code)
            .NotEmpty()
            .MaximumLength(16);
    }
}
