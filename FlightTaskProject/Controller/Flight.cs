using FlightTaskProject.Controller.Model;

namespace FlightTaskProject.Controller;

public class Flight: IFlight
{
	private readonly IFindFlight _findFligh;
	public Flight(IFindFlight findFlight)
	{
		_findFligh = findFlight;
	}
	public async Task Run(QueryModel queryParam)
	{
		await _findFligh.CreateData(queryParam);
	}
}