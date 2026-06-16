using OnibusExpress.Domain.Features.Reservations.Shared;

namespace OnibusExpress.Domain.Repositories;

public interface IPassengerRepository
{
    Task<PassengerRecordDto?> GetByCpfAsync(string cpf, CancellationToken cancellationToken = default);
    Task<PassengerRecordDto> UpsertAsync(PassengerUpsertDto passenger, CancellationToken cancellationToken = default);
}
