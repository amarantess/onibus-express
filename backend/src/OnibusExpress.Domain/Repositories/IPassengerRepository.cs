using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Domain.Repositories;

public interface IPassengerRepository
{
    Task<Passenger?> GetByCpfAsync(string cpf, CancellationToken cancellationToken = default);
    Task CreateAsync(Passenger passenger, CancellationToken cancellationToken = default);
    void Update(Passenger passenger);
}
