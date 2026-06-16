using Microsoft.EntityFrameworkCore;
using OnibusExpress.Domain.Features.Routes.ListRoutes;
using OnibusExpress.Domain.Repositories;
using OnibusExpress.Infrastructure.DataAccess;

namespace OnibusExpress.Infrastructure.Repositories;

public sealed class RouteRepository : IRouteRepository
{
    private readonly OnibusExpressDbContext _dbContext;

    public RouteRepository(OnibusExpressDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<RouteSummaryResponse>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Routes
            .Where(route => !route.IsDeleted)
            .OrderBy(route => route.Origin)
            .ThenBy(route => route.Destination)
            .Select(route => new RouteSummaryResponse
            {
                Id = route.Id,
                Origin = route.Origin,
                Destination = route.Destination,
                EstimatedDuration = route.EstimatedDuration
            })
            .ToListAsync(cancellationToken);
    }
}
