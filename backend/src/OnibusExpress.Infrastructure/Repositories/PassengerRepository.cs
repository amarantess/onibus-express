using Microsoft.EntityFrameworkCore;

using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Repositories;
using OnibusExpress.Infrastructure.DataAccess;

namespace OnibusExpress.Infrastructure.Repositories;

public sealed class PassengerRepository : IPassengerRepository
{
    private readonly OnibusExpressDbContext _dbContext;

    public PassengerRepository(OnibusExpressDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Passenger?> GetByCpfAsync(string cpf, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Passengers
            .Where(passenger => passenger.Cpf == cpf)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task CreateAsync(Passenger passenger, CancellationToken cancellationToken = default)
    {
        await _dbContext.Passengers.AddAsync(passenger, cancellationToken);
    }

    public void Update(Passenger passenger)
    {
        _dbContext.Passengers.Update(passenger);
    }
}
