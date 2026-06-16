using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Infrastructure.DataAccess.Configurations;

internal sealed class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable("Passengers");

        builder.HasKey(passenger => passenger.Id);

        builder.Property(passenger => passenger.CreatedAt)
            .IsRequired();

        builder.Property(passenger => passenger.UpdatedAt);

        builder.Property(passenger => passenger.IsDeleted)
            .IsRequired();

        builder.Property(passenger => passenger.DeletedAt);

        builder.Property(passenger => passenger.FullName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(passenger => passenger.Cpf)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(passenger => passenger.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(passenger => passenger.BirthDate)
            .IsRequired();

        builder.HasIndex(passenger => passenger.Cpf)
            .IsUnique();

        builder.HasMany(passenger => passenger.Reservations)
            .WithOne(reservation => reservation.Passenger)
            .HasForeignKey(reservation => reservation.PassengerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
