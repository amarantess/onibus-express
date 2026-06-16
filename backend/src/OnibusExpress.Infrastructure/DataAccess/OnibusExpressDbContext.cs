using Microsoft.EntityFrameworkCore;
using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Infrastructure.DataAccess;

public sealed class OnibusExpressDbContext(DbContextOptions<OnibusExpressDbContext> options) : DbContext(options)
{
    public DbSet<Route> Routes { get; set; }
    public DbSet<Trip> Trips { get; set; }
	public DbSet<Passenger> Passengers { get; set; }
	public DbSet<Reservation> Reservations { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnibusExpressDbContext).Assembly);
    }
}
