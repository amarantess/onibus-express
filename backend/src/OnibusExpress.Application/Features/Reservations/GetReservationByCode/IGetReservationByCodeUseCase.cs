using OnibusExpress.Domain.Features.Reservations.GetReservationByCode;

namespace OnibusExpress.Application.Features.Reservations.GetReservationByCode;

public interface IGetReservationByCodeUseCase
{
    Task<GetReservationByCodeResponse> ExecuteAsync(GetReservationByCodeRequest request, CancellationToken cancellationToken = default);
}
