using System.Collections;
using FlightTaskProject.DataContext;
using FlightTaskProject.Extensions.CreateCSVFile;
using Microsoft.EntityFrameworkCore;

namespace FlightTaskProject.Business;

public class CheckFlight
{
	private readonly FlightTaskDbContext _context;
	private readonly ICreateCSVFile _createCsv;

	public CheckFlight(FlightTaskDbContext context, ICreateCSVFile createCsv)
	{
			_context = context;
			_createCsv = createCsv;
	}

	/// <summary>
	/// Create Data and File 
	/// </summary>
	/// <param name="param">Query Parameters</param>
	/// <returns></returns>
	public async Task CreateData(QueryParam param)
	{
		await FindOriginAndDestinationForAgency(param);
		var data = await FindAllData(param);
		 
		await _createCsv.CreateFile(data);
		Console.WriteLine("The data was successfully saved in a CSV file.");
	}

	/// <summary>
	/// Find Origin CityIds and Destination CityIds for Agency
	/// </summary>
	/// <param name="queryParam">Query Parameters</param>
	/// <returns></returns>
	private async Task FindOriginAndDestinationForAgency(QueryParam queryParam)
	{
		queryParam.QueryParamResult = new QueryParamResult(); 

		try
		{
			var subInfo =
				_context.Subscriptions
					.Where(s => s.AgencyId == queryParam.AgencyId)
					.Select(s => new
					{
						s.DestinationCityId,
						s.OriginCityId,
					});

			var list = await subInfo.ToListAsync();

			foreach (var l in list)
			{
				queryParam.QueryParamResult.DestinationCityIds.Add(l.DestinationCityId);
				queryParam.QueryParamResult.OriginCityIds.Add(l.OriginCityId);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("Error: " + e.Message);
		}
	}

	/// <summary>
	/// Find All Data for 
	/// </summary>
	/// <param name="param">Query Parameters</param>
	/// <returns></returns>
	private async Task<IEnumerable> FindAllData(QueryParam param)
	{
		IEnumerable flightInfo = null;
		try
		{
			    flightInfo =
				from fl in _context.Flights
				join rt in _context.Routes on fl.RouteId equals rt.RouteId
				where rt.DepartureDate <= param.EndDate && rt.DepartureDate >= param.StartDate &&
				      param.QueryParamResult.DestinationCityIds.Contains(rt.DestinationCityId) && param.QueryParamResult.OriginCityIds.Contains(rt.OriginCityId)
				select new
				{
					fl.FlightId,
					rt.OriginCityId,
					rt.DestinationCityId,
					fl.DepartureTime,
					fl.ArrivalTime,
					fl.AirlineId,
					Status = "new/Discontinued"
				};
		}
		catch (Exception e)
		{
			Console.WriteLine("Error:" + e.Message);
		}

		return flightInfo;
	}
}