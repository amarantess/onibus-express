using FluentValidation;
using OnibusExpress.Domain.Features.Routes.ListRoutes;
using OnibusExpress.Domain.Repositories;

namespace OnibusExpress.Application.Features.Routes.ListRoutes;

public sealed class ListRoutesUseCase : IListRoutesUseCase
{
    private readonly IValidator<ListRoutesRequest> _validator;
    private readonly IRouteRepository _routeRepository;

    public ListRoutesUseCase(IValidator<ListRoutesRequest> validator, IRouteRepository routeRepository)
    {
        _validator = validator;
        _routeRepository = routeRepository;
    }

    public async Task<ListRoutesResponse> ExecuteAsync(ListRoutesRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var routes = await _routeRepository.ListAsync(cancellationToken);

        return new ListRoutesResponse
        {
            Routes = routes
        };
    }
}
