using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Infrastructure.DataAccess.Configurations;

internal sealed class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips");

        builder.HasKey(trip => trip.Id);

        builder.Property(trip => trip.CreatedAt)
            .IsRequired();

        builder.Property(trip => trip.UpdatedAt);

        builder.Property(trip => trip.IsDeleted)
            .IsRequired();

        builder.Property(trip => trip.DeletedAt);

        builder.Property(trip => trip.DepartureAt)
            .IsRequired();

        builder.Property(trip => trip.BasePrice)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(trip => trip.AvailableSeats)
            .IsRequired();

        builder.HasIndex(trip => trip.DepartureAt);

        builder.HasMany(trip => trip.Reservations)
            .WithOne(reservation => reservation.Trip)
            .HasForeignKey(reservation => reservation.TripId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
