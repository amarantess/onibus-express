using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Tests.TestUtilities.Builders;

internal static class ReservationBuilder
{
    public static Reservation Build(Trip trip, Passenger passenger, int seatNumber, string code, DateTimeOffset createdAt)
    {
        return new Reservation
        {
            Id = Guid.NewGuid(),
            Code = code,
            TripId = trip.Id,
            Trip = trip,
            PassengerId = passenger.Id,
            Passenger = passenger,
            SeatNumber = seatNumber,
            Status = ReservationStatus.Confirmed,
            CreatedAt = createdAt
        };
    }
}
