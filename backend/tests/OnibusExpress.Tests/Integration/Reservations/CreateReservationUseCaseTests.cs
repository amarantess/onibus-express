using FluentAssertions;

using OnibusExpress.Domain.Enums;
using OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;
using OnibusExpress.Tests.TestUtilities.Builders;
using OnibusExpress.Tests.TestUtilities.Database;
using OnibusExpress.Tests.TestUtilities.Fakes;
using OnibusExpress.Tests.TestUtilities.UseCases;

namespace OnibusExpress.Tests.Integration.Reservations;

public sealed class CreateReservationUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCreateReservation_WhenPassengerDoesNotExist()
    {
        using var database = new InMemorySqliteDatabase();
        var now = new DateTimeOffset(2026, 6, 17, 10, 0, 0, TimeSpan.Zero);
        var route = RouteBuilder.Build(now);
        var trip = TripBuilder.Build(route, now.AddHours(6), now);
        database.Context.Routes.Add(route);
        database.Context.Trips.Add(trip);
        await database.Context.SaveChangesAsync();

        var request = CreateReservationRequestBuilder.Build(trip.Id);
        var useCase = ReservationUseCaseFactory.CreateReservationUseCase(database.Context, now);

        var response = await useCase.ExecuteAsync(request);

        response.Code.Should().Be("ABC-12345");
        response.Status.Should().Be(ReservationStatus.Confirmed);
        database.Context.Reservations.Should().ContainSingle(reservation => reservation.Code == response.Code);
        database.Context.Passengers.Should().ContainSingle(passenger => passenger.Cpf == "52998224725");
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowConflictException_WhenSeatIsAlreadyOccupied()
    {
        using var database = new InMemorySqliteDatabase();
        var now = new DateTimeOffset(2026, 6, 17, 10, 0, 0, TimeSpan.Zero);
        var route = RouteBuilder.Build(now);
        var trip = TripBuilder.Build(route, now.AddHours(6), now);
        var passenger = PassengerBuilder.Build(now);
        var reservation = ReservationBuilder.Build(trip, passenger, 1, "AAA-11111", now);
        database.Context.AddRange(route, trip, passenger, reservation);
        await database.Context.SaveChangesAsync();

        var request = CreateReservationRequestBuilder.Build(trip.Id);
        var useCase = ReservationUseCaseFactory.CreateReservationUseCase(database.Context, now);

        Func<Task> act = async () => await useCase.ExecuteAsync(request);

        await act.Should().ThrowAsync<ConflictException>()
            .WithMessage("The selected seat is already occupied.");
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowConflictException_WhenTripAlreadyDeparted()
    {
        using var database = new InMemorySqliteDatabase();
        var now = new DateTimeOffset(2026, 6, 17, 10, 0, 0, TimeSpan.Zero);
        var route = RouteBuilder.Build(now);
        var trip = TripBuilder.Build(route, now.AddMinutes(-1), now);
        database.Context.AddRange(route, trip);
        await database.Context.SaveChangesAsync();

        var request = CreateReservationRequestBuilder.Build(trip.Id);
        var useCase = ReservationUseCaseFactory.CreateReservationUseCase(database.Context, now);

        Func<Task> act = async () => await useCase.ExecuteAsync(request);

        await act.Should().ThrowAsync<ConflictException>()
            .WithMessage("Cannot create a reservation for a trip that has already departed.");
    }

    [Fact]
    public async Task ExecuteAsync_ShouldGenerateAnotherCode_WhenFirstCodeAlreadyExists()
    {
        using var database = new InMemorySqliteDatabase();
        var now = new DateTimeOffset(2026, 6, 17, 10, 0, 0, TimeSpan.Zero);
        var route = RouteBuilder.Build(now);
        var trip = TripBuilder.Build(route, now.AddHours(6), now);
        var passenger = PassengerBuilder.Build(now);
        var existingReservation = ReservationBuilder.Build(trip, passenger, 2, "ABC-12345", now);
        database.Context.AddRange(route, trip, passenger, existingReservation);
        await database.Context.SaveChangesAsync();

        var request = CreateReservationRequestBuilder.Build(trip.Id);
        request = new()
        {
            TripId = request.TripId,
            SeatNumber = 3,
            FullName = request.FullName,
            Cpf = "111.444.777-35",
            Email = "joao.silva@example.com",
            BirthDate = request.BirthDate
        };
        var codeGenerator = new SequentialReservationCodeGenerator("ABC-12345", "DEF-67890");
        var useCase = ReservationUseCaseFactory.CreateReservationUseCase(database.Context, now, codeGenerator);

        var response = await useCase.ExecuteAsync(request);

        response.Code.Should().Be("DEF-67890");
        database.Context.Reservations.Should().Contain(reservation => reservation.Code == "DEF-67890");
    }
}
