namespace OnibusExpress.Domain.Features.Routes.ListRoutes;

public sealed class RouteSummaryResponse
{
    public Guid Id { get; init; }
    public string Origin { get; init; } = string.Empty;
    public string Destination { get; init; } = string.Empty;
    public TimeSpan EstimatedDuration { get; init; }
}
