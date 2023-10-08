namespace FlightTaskProject.Models;

public partial class Subscription
{
    public int Id { get; set; }

    public int AgencyId { get; set; }

    public int OriginCityId { get; set; }

    public int DestinationCityId { get; set; }
}
