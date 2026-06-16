namespace OnibusExpress.Domain.Features.Trips.GetTripDetails;

public sealed class SeatAvailabilityResponse
{
    public int SeatNumber { get; init; }
    public bool IsOccupied { get; init; }
}
