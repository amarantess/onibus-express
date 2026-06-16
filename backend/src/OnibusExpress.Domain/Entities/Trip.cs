namespace OnibusExpress.Domain.Entities;

public sealed class Trip : BaseEntity
{
    public Guid RouteId { get; set; }
    public DateTimeOffset DepartureAt { get; set; }
    public decimal BasePrice { get; set; }
    public int AvailableSeats { get; set; }
    public Route? Route { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = [];
}
