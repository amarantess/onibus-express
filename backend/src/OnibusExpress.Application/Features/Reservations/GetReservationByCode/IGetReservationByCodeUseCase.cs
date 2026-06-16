using OnibusExpress.Domain.Features.Reservations.GetReservationByCode;

namespace OnibusExpress.Application.Features.Reservations.GetReservationByCode;

public interface IGetReservationByCodeUseCase
{
    Task<GetReservationByCodeResponse> ExecuteAsync(string code, CancellationToken cancellationToken = default);
}
