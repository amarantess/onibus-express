using Microsoft.EntityFrameworkCore;
using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Infrastructure.DataAccess;

public sealed class OnibusExpressDbContext : DbContext
{
    public OnibusExpressDbContext(DbContextOptions<OnibusExpressDbContext> options) : base(options)
    {
    }

    public DbSet<Route> Routes { get; set; } = null!;
    public DbSet<Trip> Trips { get; set; } = null!;
    public DbSet<Passenger> Passengers { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnibusExpressDbContext).Assembly);
    }
}
