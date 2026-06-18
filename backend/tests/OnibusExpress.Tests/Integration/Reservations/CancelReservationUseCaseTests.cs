using FluentAssertions;

using OnibusExpress.Domain.Enums;
using OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;
using OnibusExpress.Tests.TestUtilities.Builders;
using OnibusExpress.Tests.TestUtilities.Database;
using OnibusExpress.Tests.TestUtilities.UseCases;

namespace OnibusExpress.Tests.Integration.Reservations;

public sealed class CancelReservationUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCancelReservation_WhenDepartureIsMoreThanTwoHoursAway()
    {
        using var database = new InMemorySqliteDatabase();
        var now = new DateTimeOffset(2026, 6, 17, 10, 0, 0, TimeSpan.Zero);
        var route = RouteBuilder.Build(now);
        var trip = TripBuilder.Build(route, now.AddHours(3), now);
        var passenger = PassengerBuilder.Build(now);
        var reservation = ReservationBuilder.Build(trip, passenger, 1, "ABC-12345", now);
        database.Context.AddRange(route, trip, passenger, reservation);
        await database.Context.SaveChangesAsync();

        var useCase = ReservationUseCaseFactory.CancelReservationUseCase(database.Context, now);

        var response = await useCase.ExecuteAsync("ABC-12345");

        response.Status.Should().Be(ReservationStatus.Cancelled);
        response.CancelledAt.Should().Be(now);
        database.Context.Reservations.Single().IsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowConflictException_WhenDepartureIsLessThanTwoHoursAway()
    {
        using var database = new InMemorySqliteDatabase();
        var now = new DateTimeOffset(2026, 6, 17, 10, 0, 0, TimeSpan.Zero);
        var route = RouteBuilder.Build(now);
        var trip = TripBuilder.Build(route, now.AddHours(1), now);
        var passenger = PassengerBuilder.Build(now);
        var reservation = ReservationBuilder.Build(trip, passenger, 1, "ABC-12345", now);
        database.Context.AddRange(route, trip, passenger, reservation);
        await database.Context.SaveChangesAsync();

        var useCase = ReservationUseCaseFactory.CancelReservationUseCase(database.Context, now);

        Func<Task> act = async () => await useCase.ExecuteAsync("ABC-12345");

        await act.Should().ThrowAsync<ConflictException>()
            .WithMessage("Cancellation is only allowed up to 2 hours before departure.");
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowConflictException_WhenReservationIsAlreadyCancelled()
    {
        using var database = new InMemorySqliteDatabase();
        var now = new DateTimeOffset(2026, 6, 17, 10, 0, 0, TimeSpan.Zero);
        var route = RouteBuilder.Build(now);
        var trip = TripBuilder.Build(route, now.AddHours(3), now);
        var passenger = PassengerBuilder.Build(now);
        var reservation = ReservationBuilder.Build(trip, passenger, 1, "ABC-12345", now);
        reservation.Status = ReservationStatus.Cancelled;
        reservation.IsDeleted = true;
        database.Context.AddRange(route, trip, passenger, reservation);
        await database.Context.SaveChangesAsync();

        var useCase = ReservationUseCaseFactory.CancelReservationUseCase(database.Context, now);

        Func<Task> act = async () => await useCase.ExecuteAsync("ABC-12345");

        await act.Should().ThrowAsync<ConflictException>()
            .WithMessage("Reservation is already cancelled.");
    }
}
