using Microsoft.EntityFrameworkCore;

using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Infrastructure.DataAccess.Seed;

public sealed class OnibusExpressDbSeeder
{
    private readonly OnibusExpressDbContext _dbContext;

    public static readonly Guid SaoPauloRioRouteId = Guid.Parse("4f2de09c-31a6-4a64-a14b-5d5ef7141001");
    public static readonly Guid CuritibaFlorianopolisRouteId = Guid.Parse("4f2de09c-31a6-4a64-a14b-5d5ef7141002");
    public static readonly Guid BeloHorizonteVitoriaRouteId = Guid.Parse("4f2de09c-31a6-4a64-a14b-5d5ef7141003");
    public static readonly Guid SaoPauloRioFutureTripId = Guid.Parse("8f0ca1f9-7b39-4b2c-a25e-5d5ef7142001");
    public static readonly Guid CuritibaFlorianopolisNearTripId = Guid.Parse("8f0ca1f9-7b39-4b2c-a25e-5d5ef7142002");
    public static readonly Guid BeloHorizonteVitoriaFutureTripId = Guid.Parse("8f0ca1f9-7b39-4b2c-a25e-5d5ef7142003");
    public static readonly Guid SaoPauloRioPastTripId = Guid.Parse("8f0ca1f9-7b39-4b2c-a25e-5d5ef7142004");

    public OnibusExpressDbSeeder(OnibusExpressDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.EnsureCreatedAsync(cancellationToken);

        if (await _dbContext.Routes.AnyAsync(cancellationToken) || await _dbContext.Trips.AnyAsync(cancellationToken))
        {
            return;
        }

        var createdAt = DateTimeOffset.UtcNow;

        var routeSaoPauloRio = new Route
        {
            Id = SaoPauloRioRouteId,
            Origin = "Sao Paulo",
            Destination = "Rio de Janeiro",
            EstimatedDuration = TimeSpan.FromHours(6),
            CreatedAt = createdAt
        };

        var routeCuritibaFlorianopolis = new Route
        {
            Id = CuritibaFlorianopolisRouteId,
            Origin = "Curitiba",
            Destination = "Florianopolis",
            EstimatedDuration = TimeSpan.FromHours(4),
            CreatedAt = createdAt
        };

        var routeBeloHorizonteVitoria = new Route
        {
            Id = BeloHorizonteVitoriaRouteId,
            Origin = "Belo Horizonte",
            Destination = "Vitoria",
            EstimatedDuration = TimeSpan.FromHours(7),
            CreatedAt = createdAt
        };

        var trips = new[]
        {
            new Trip
            {
                Id = SaoPauloRioFutureTripId,
                RouteId = routeSaoPauloRio.Id,
                DepartureAt = new DateTimeOffset(createdAt.UtcDateTime.Date.AddDays(1).AddHours(12), TimeSpan.Zero),
                BasePrice = 159.90m,
                AvailableSeats = 46,
                CreatedAt = createdAt
            },
            new Trip
            {
                Id = CuritibaFlorianopolisNearTripId,
                RouteId = routeCuritibaFlorianopolis.Id,
                DepartureAt = createdAt.AddHours(1),
                BasePrice = 92.50m,
                AvailableSeats = 40,
                CreatedAt = createdAt
            },
            new Trip
            {
                Id = BeloHorizonteVitoriaFutureTripId,
                RouteId = routeBeloHorizonteVitoria.Id,
                DepartureAt = new DateTimeOffset(createdAt.UtcDateTime.Date.AddDays(2).AddHours(9), TimeSpan.Zero),
                BasePrice = 129.99m,
                AvailableSeats = 44,
                CreatedAt = createdAt
            },
            new Trip
            {
                Id = SaoPauloRioPastTripId,
                RouteId = routeSaoPauloRio.Id,
                DepartureAt = createdAt.AddDays(-1),
                BasePrice = 149.90m,
                AvailableSeats = 46,
                CreatedAt = createdAt
            }
        };

        await _dbContext.Routes.AddRangeAsync(routeSaoPauloRio, routeCuritibaFlorianopolis, routeBeloHorizonteVitoria);
        await _dbContext.Trips.AddRangeAsync(trips, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
