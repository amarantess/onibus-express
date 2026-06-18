using OnibusExpress.Domain.Features.Reservations.CreateReservation;

namespace OnibusExpress.Tests.TestUtilities.Builders;

internal static class CreateReservationRequestBuilder
{
    public static CreateReservationRequest Build(Guid tripId)
    {
        return new CreateReservationRequest
        {
            TripId = tripId,
            SeatNumber = 1,
            FullName = "Maria Silva",
            Cpf = "529.982.247-25",
            Email = "maria.silva@example.com",
            BirthDate = new DateOnly(1995, 6, 15)
        };
    }
}
