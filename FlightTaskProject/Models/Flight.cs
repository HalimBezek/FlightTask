namespace FlightTaskProject.Models;

public partial class Flight
{
    public int Id { get; set; }

    public int FlightId { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public int AirlineId { get; set; }

    public int RouteId { get; set; }

    public virtual Route Route { get; set; } = null!;
}
