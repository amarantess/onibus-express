namespace OnibusExpress.Domain.Features.Reservations.GetReservationByCode;

public sealed class PassengerReservationResponse
{
    public Guid PassengerId { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateOnly BirthDate { get; init; }
}
