using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Tests.TestUtilities.Builders;

internal static class TripBuilder
{
    public static Trip Build(Route route, DateTimeOffset departureAt, DateTimeOffset createdAt)
    {
        return new Trip
        {
            Id = Guid.NewGuid(),
            RouteId = route.Id,
            Route = route,
            DepartureAt = departureAt,
            BasePrice = 159.90m,
            AvailableSeats = 46,
            CreatedAt = createdAt
        };
    }
}
