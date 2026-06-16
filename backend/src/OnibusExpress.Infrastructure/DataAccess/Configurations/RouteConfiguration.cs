using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Infrastructure.DataAccess.Configurations;

internal sealed class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable("Routes");

        builder.HasKey(route => route.Id);

        builder.Property(route => route.CreatedAt)
            .IsRequired();

        builder.Property(route => route.UpdatedAt);

        builder.Property(route => route.IsDeleted)
            .IsRequired();

        builder.Property(route => route.DeletedAt);

        builder.Property(route => route.Origin)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(route => route.Destination)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(route => route.EstimatedDuration)
            .IsRequired();

        builder.HasIndex(route => route.Origin);
        builder.HasIndex(route => route.Destination);

        builder.HasMany(route => route.Trips)
            .WithOne(trip => trip.Route)
            .HasForeignKey(trip => trip.RouteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
