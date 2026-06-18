using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OnibusExpress.Domain.Repositories;
using OnibusExpress.Infrastructure.DataAccess;
using OnibusExpress.Infrastructure.DataAccess.Seed;
using OnibusExpress.Infrastructure.Repositories;

namespace OnibusExpress.Infrastructure;

public static class DependencyInjectionExtensionInfra
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddUnitOfWork();
        services.AddSeed();

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OnibusExpressDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(DataAccess.OnibusExpressDbContext).Assembly.FullName)));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPassengerRepository, PassengerRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<IRouteRepository, RouteRepository>();
    }

    private static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddSeed(this IServiceCollection services)
    {
        services.AddScoped<OnibusExpressDbSeeder>();
    }
}
