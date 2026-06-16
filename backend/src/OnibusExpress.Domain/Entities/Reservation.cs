using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Domain.Entities;

public sealed class Reservation : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public Guid TripId { get; set; }
    public Guid PassengerId { get; set; }
    public int SeatNumber { get; set; }
    public ReservationStatus Status { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }
    public Trip? Trip { get; set; }
    public Passenger? Passenger { get; set; }
}
