using Microsoft.Extensions.Configuration;

namespace OnibusExpress.Infrastructure.DataAccess;

public static class ConnectionStringResolver
{
    private const string DefaultConnectionName = "DefaultConnection";

    public static string GetRequiredDefaultConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DefaultConnectionName);

        if (string.IsNullOrWhiteSpace(connectionString) || connectionString.Contains("<"))
        {
            throw new InvalidOperationException(
                "A valid 'ConnectionStrings:DefaultConnection' must be configured in appsettings.json or appsettings.Development.json.");
        }

        return connectionString;
    }

    public static IConfiguration BuildDesignTimeConfiguration()
    {
        var apiProjectDirectory = FindApiProjectDirectory();

        return new ConfigurationBuilder()
            .SetBasePath(apiProjectDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
    }

    private static string FindApiProjectDirectory()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (currentDirectory is not null)
        {
            var candidate = Path.Combine(currentDirectory.FullName, "backend", "src", "OnibusExpress.Api");
            if (Directory.Exists(candidate))
            {
                return candidate;
            }

            currentDirectory = currentDirectory.Parent;
        }

        throw new InvalidOperationException("Could not locate the OnibusExpress.Api project directory to load appsettings files.");
    }
}
