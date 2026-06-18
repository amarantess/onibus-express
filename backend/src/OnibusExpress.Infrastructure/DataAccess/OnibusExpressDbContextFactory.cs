using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OnibusExpress.Infrastructure.DataAccess;

public sealed class OnibusExpressDbContextFactory : IDesignTimeDbContextFactory<OnibusExpressDbContext>
{
    public OnibusExpressDbContext CreateDbContext(string[] args)
    {
        var configuration = ConnectionStringResolver.BuildDesignTimeConfiguration();
        var connectionString = ConnectionStringResolver.GetRequiredDefaultConnectionString(configuration);

        var optionsBuilder = new DbContextOptionsBuilder<OnibusExpressDbContext>();
        optionsBuilder.UseSqlServer(
            connectionString,
            options => options.MigrationsAssembly(typeof(OnibusExpressDbContext).Assembly.FullName));

        return new OnibusExpressDbContext(optionsBuilder.Options);
    }
}
