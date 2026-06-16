namespace OnibusExpress.Domain.Entities;

public sealed class Passenger : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = [];
}
