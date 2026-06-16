using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Infrastructure.DataAccess.Configurations;

internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(reservation => reservation.Id);

        builder.Property(reservation => reservation.CreatedAt)
            .IsRequired();

        builder.Property(reservation => reservation.UpdatedAt);

        builder.Property(reservation => reservation.IsDeleted)
            .IsRequired();

        builder.Property(reservation => reservation.DeletedAt);

        builder.Property(reservation => reservation.Code)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(reservation => reservation.SeatNumber)
            .IsRequired();

        builder.Property(reservation => reservation.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(reservation => reservation.CancelledAt);

        builder.HasIndex(reservation => reservation.Code)
            .IsUnique();

        builder.HasIndex(reservation => new { reservation.TripId, reservation.SeatNumber })
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");
    }
}
