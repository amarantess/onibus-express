using OnibusExpress.Domain.Repositories;

namespace OnibusExpress.Infrastructure.DataAccess;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly OnibusExpressDbContext _dbContext;

    public UnitOfWork(OnibusExpressDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
