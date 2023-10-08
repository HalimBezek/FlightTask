namespace FlightTaskProject.Models;

public partial class Route
{
    public int Id { get; set; }

    public int RouteId { get; set; }

    public int DestinationCityId { get; set; }

    public DateTime? DepartureDate { get; set; }

    public int? OriginCityId { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
