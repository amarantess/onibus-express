using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Domain.Features.Reservations.Shared;

public sealed class ReservationDetailsDto
{
    public string Code { get; init; } = string.Empty;
    public ReservationStatus Status { get; init; }
    public int SeatNumber { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? CancelledAt { get; init; }
    public bool IsDeleted { get; init; }
    public Guid PassengerId { get; init; }
    public string PassengerFullName { get; init; } = string.Empty;
    public string PassengerCpf { get; init; } = string.Empty;
    public string PassengerEmail { get; init; } = string.Empty;
    public DateOnly PassengerBirthDate { get; init; }
    public Guid TripId { get; init; }
    public Guid RouteId { get; init; }
    public string Origin { get; init; } = string.Empty;
    public string Destination { get; init; } = string.Empty;
    public DateTimeOffset DepartureAt { get; init; }
    public decimal BasePrice { get; init; }
}
