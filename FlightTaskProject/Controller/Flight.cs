using FlightTaskProject.Business.Model;
using FlightTaskProject.DataContext;
using FlightTaskProject.Extensions.CreateCSVFile;
using Microsoft.EntityFrameworkCore;

namespace FlightTaskProject.Business;

public class Flight
{
	private readonly FindFlight _findFligh;
	public Flight(FindFlight findFlight)
	{
		_findFligh = findFlight;
	}
	public async Task Run(QueryParam queryParam)
	{
		await _findFligh.CreateData(queryParam);
	}
}