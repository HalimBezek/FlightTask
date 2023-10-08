namespace FlightTaskProject.Business;

public class QueryParam
{
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public int AgencyId { get; set; }
	public QueryParamResult QueryParamResult { get; set; }

}

public class QueryParamResult
{
	public List<int> DestinationCityIds { get; set; }
	public List<int?> OriginCityIds { get; set; }
}