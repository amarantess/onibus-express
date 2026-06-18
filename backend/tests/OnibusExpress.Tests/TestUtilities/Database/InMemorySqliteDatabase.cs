using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using OnibusExpress.Infrastructure.DataAccess;

namespace OnibusExpress.Tests.TestUtilities.Database;

internal sealed class InMemorySqliteDatabase : IDisposable
{
    private readonly SqliteConnection _connection;

    public InMemorySqliteDatabase()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        Context = CreateContext();
        Context.Database.EnsureCreated();
    }

    public OnibusExpressDbContext Context { get; }

    public OnibusExpressDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<OnibusExpressDbContext>()
            .UseSqlite(_connection)
            .Options;

        return new OnibusExpressDbContext(options);
    }

    public void Dispose()
    {
        Context.Dispose();
        _connection.Dispose();
    }
}
