using OnibusExpress.Domain.Features.Routes.ListRoutes;

namespace OnibusExpress.Domain.Repositories;

public interface IRouteRepository
{
    Task<IReadOnlyCollection<RouteSummaryResponse>> ListAsync(CancellationToken cancellationToken = default);
}
