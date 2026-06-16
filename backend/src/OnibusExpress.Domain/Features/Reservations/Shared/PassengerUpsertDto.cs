namespace OnibusExpress.Domain.Features.Reservations.Shared;

public sealed class PassengerUpsertDto
{
    public string FullName { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateOnly BirthDate { get; init; }
}
