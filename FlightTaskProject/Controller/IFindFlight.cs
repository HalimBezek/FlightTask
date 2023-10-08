using FlightTaskProject.Business.Model;
using FlightTaskProject.Business;
using FlightTaskProject.Controller.Model;

namespace FlightTaskProject.Controller
{
    public interface IFindFlight
	{
		public Task CreateData(QueryModel param);
		public Task<List<ResultModel>?> FindAllData(QueryModel param);
		public Task FindOriginAndDestinationForAgency(QueryModel queryParam);
	}
}
