namespace FlightTaskProject.Controller.Model;

public class QueryModel
{
    public QueryModel(QueryResultModel queryResultModel)
    {
        QueryResultModel = queryResultModel;
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int AgencyId { get; set; }
    public QueryResultModel QueryResultModel { get; set; }

}

public class QueryResultModel
{
    public QueryResultModel(List<int> destinationCityIds, List<int?> originCityIds)
    {
        DestinationCityIds = destinationCityIds;
        OriginCityIds = originCityIds;
    }

    public List<int> DestinationCityIds { get; set; }
    public List<int?> OriginCityIds { get; set; }
}