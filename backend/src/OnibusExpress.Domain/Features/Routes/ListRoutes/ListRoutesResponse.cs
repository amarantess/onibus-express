namespace OnibusExpress.Domain.Features.Routes.ListRoutes;

public sealed class ListRoutesResponse
{
    public IReadOnlyCollection<RouteSummaryResponse> Routes { get; init; } = [];
}
