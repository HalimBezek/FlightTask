using System.Collections;
using FlightTaskProject.DataContext;
using FlightTaskProject.Extensions.CreateCSVFile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace FlightTaskProject.Business;

public class CheckFlight
{
	private readonly FlightTaskDbContext _context;

	public CheckFlight(FlightTaskDbContext context)
	{
			_context = context;
	}

	/// <summary>
	/// Create Data and File 
	/// </summary>
	/// <param name="param">Query Parameters</param>
	/// <returns></returns>
	public async Task CreateData(QueryParam param)
	{
		var tuple =  await FindOriginAndDestinationForAgency(param);
		param.DestinationCityIds = tuple.Item1;
		param.OriginCityIds = tuple.Item2;

		var data = FindAllData(param);

		ICreateCSVFile createCsv = new CreateCSVFile(await data);
		await createCsv.CreateFile();

		Console.WriteLine("The data was successfully saved in a CSV file.");

	}

	/// <summary>
	/// Find Origin CityIds and Destination CityIds for Agency
	/// </summary>
	/// <param name="queryParam">Query Parameters</param>
	/// <returns></returns>
	private async Task<Tuple<List<int>, List<int?>>> FindOriginAndDestinationForAgency(QueryParam queryParam)
	{
		List<int> destinationCityIds = new List<int>();//todo: set to parameters
		List<int?> originCityIds = new List<int?>();

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
				destinationCityIds.Add(l.DestinationCityId);
				originCityIds.Add(l.OriginCityId);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("Error: " + e.Message);
		}

		return new Tuple<List<int>, List<int?>>(destinationCityIds, originCityIds);
	}

	/// <summary>
	/// Find All Data for 
	/// </summary>
	/// <param name="param">Query Parameters</param>
	/// <returns></returns>
	private async Task<IEnumerable> FindAllData(QueryParam param)
	{
		IList<ResultModel> result = new List<ResultModel>();
		IEnumerable flightInfo = null;
		try
		{
			    flightInfo =
				from fl in _context.Flights
				join rt in _context.Routes on fl.RouteId equals rt.RouteId
				where rt.DepartureDate <= param.EndDate && rt.DepartureDate >= param.StartDate &&
				      param.DestinationCityIds.Contains(rt.DestinationCityId) && param.OriginCityIds.Contains(rt.OriginCityId)
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

			//var list = await flightInfo.ToListAsync();
			
			//foreach (var d in list)
			//{
			//	ResultModel c= new ResultModel
			//	{
			//		FlightId = d.FlightId,
			//		OriginCityId = d.OriginCityId,
			//		DestinationCityId = d.DestinationCityId,
			//		AirlineId = d.AirlineId,
			//		Status = d.Status,
			//		ArrivalTime = d.ArrivalTime,
			//		DepartureTime = d.DepartureTime
			//	};

			//	result.Add(c);
			//}
		}
		catch (Exception e)
		{
			Console.WriteLine("Error:" + e.Message);
		}

		return flightInfo;
	}

}