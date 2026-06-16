using OnibusExpress.Domain.Features.Routes.ListRoutes;

namespace OnibusExpress.Application.Features.Routes.ListRoutes;

public interface IListRoutesUseCase
{
    Task<ListRoutesResponse> ExecuteAsync(ListRoutesRequest request, CancellationToken cancellationToken = default);
}
