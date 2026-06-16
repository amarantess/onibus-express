using OnibusExpress.Domain.Features.Reservations.CancelReservation;

namespace OnibusExpress.Application.Features.Reservations.CancelReservation;

public interface ICancelReservationUseCase
{
    Task<CancelReservationResponse> ExecuteAsync(string code, CancellationToken cancellationToken = default);
}
