using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using OnibusExpress.Infrastructure.DataAccess.Seed;

namespace OnibusExpress.Infrastructure.DataAccess;

public static class DatabaseInitializationExtensions
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<OnibusExpressDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        var seeder = scope.ServiceProvider.GetRequiredService<OnibusExpressDbSeeder>();
        await seeder.SeedAsync(cancellationToken);
    }
}
