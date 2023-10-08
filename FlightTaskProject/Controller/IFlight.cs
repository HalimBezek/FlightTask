using FlightTaskProject.Controller.Model;

namespace FlightTaskProject.Controller
{
    public interface IFlight
	{
		public Task Run(QueryModel queryParam);
	}
}
