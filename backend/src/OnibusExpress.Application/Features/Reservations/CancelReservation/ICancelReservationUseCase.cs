using OnibusExpress.Domain.Features.Reservations.CancelReservation;

namespace OnibusExpress.Application.Features.Reservations.CancelReservation;

public interface ICancelReservationUseCase
{
    Task<CancelReservationResponse> ExecuteAsync(CancelReservationRequest request, CancellationToken cancellationToken = default);
}
