using OnibusExpress.Domain.Features.Reservations.CreateReservation;

namespace OnibusExpress.Application.Features.Reservations.CreateReservation;

public interface ICreateReservationUseCase
{
    Task<CreateReservationResponse> ExecuteAsync(CreateReservationRequest request, CancellationToken cancellationToken = default);
}
