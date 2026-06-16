namespace OnibusExpress.Domain.Features.Reservations.GetReservationByCode;

public sealed class GetReservationByCodeRequest
{
    public string Code { get; init; } = string.Empty;
}
