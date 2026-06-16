namespace OnibusExpress.Domain.Features.Reservations.CreateReservation;

public sealed class CreateReservationRequest
{
    public Guid TripId { get; init; }
    public int SeatNumber { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateOnly BirthDate { get; init; }
}
