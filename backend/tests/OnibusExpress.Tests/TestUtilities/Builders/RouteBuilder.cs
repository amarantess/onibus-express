using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Tests.TestUtilities.Builders;

internal static class RouteBuilder
{
    public static Route Build(DateTimeOffset createdAt)
    {
        return new Route
        {
            Id = Guid.NewGuid(),
            Origin = "Sao Paulo",
            Destination = "Rio de Janeiro",
            EstimatedDuration = TimeSpan.FromHours(6),
            CreatedAt = createdAt
        };
    }
}
