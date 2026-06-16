using FluentValidation;
using OnibusExpress.Domain.Features.Reservations.CreateReservation;

namespace OnibusExpress.Application.Features.Reservations.CreateReservation;

public sealed class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(request => request.TripId)
            .NotEmpty();

        RuleFor(request => request.SeatNumber)
            .GreaterThan(0);

        RuleFor(request => request.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(request => request.Cpf)
            .NotEmpty()
            .MaximumLength(14);

        RuleFor(request => request.Email)
            .NotEmpty()
            .MaximumLength(200)
            .EmailAddress();

        RuleFor(request => request.BirthDate)
            .NotEqual(default(DateOnly));
    }
}
