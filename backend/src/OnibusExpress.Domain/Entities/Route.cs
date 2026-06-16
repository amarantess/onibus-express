namespace OnibusExpress.Domain.Entities;

public sealed class Route : BaseEntity
{
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public TimeSpan EstimatedDuration { get; set; }
    public ICollection<Trip> Trips { get; set; } = [];
}
