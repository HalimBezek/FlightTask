using FlightTaskProject.Controller.Model;
using FlightTaskProject.DataContext;
using FlightTaskProject.Extensions.CreateCSVFile;
using Microsoft.EntityFrameworkCore;

namespace FlightTaskProject.Controller
{
    public class FindFlight: IFindFlight
	{
		private readonly FlightTaskDbContext _context;
		private readonly ICreateCSVFile _createCsv;
		private readonly IFlightStatus _flightStatus;

		public FindFlight(FlightTaskDbContext context, ICreateCSVFile createCsv, IFlightStatus flightStatus)
		{
			_context = context;
			_createCsv = createCsv;
			_flightStatus = flightStatus;
		}

		/// <summary>
		/// Create Data and File 
		/// </summary>
		/// <param name="param">Query Parameters</param>
		/// <returns></returns>
		public async Task CreateData(QueryModel param)
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
		public async Task FindOriginAndDestinationForAgency(QueryModel queryParam)
		{
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
					queryParam.QueryResultModel.DestinationCityIds.Add(l.DestinationCityId);
					queryParam.QueryResultModel.OriginCityIds.Add(l.OriginCityId);
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
		public async Task<List<ResultModel>?> FindAllData(QueryModel param)
		{
			List<ResultModel>? flightInfoList = null;

			try
			{
				IQueryable<ResultModel> flightInfo =
					from fl in _context.Flights
					join rt in _context.Routes on fl.RouteId equals rt.RouteId
					where rt.DepartureDate <= param.EndDate && rt.DepartureDate >= param.StartDate &&
						  param.QueryResultModel.DestinationCityIds.Contains(rt.DestinationCityId) && param.QueryResultModel.OriginCityIds.Contains(rt.OriginCityId)
					orderby fl.DepartureTime
					select new ResultModel
					{
						flight_id = fl.FlightId,
						origin_city_id = (int)rt.OriginCityId,
						destination_city_id = rt.DestinationCityId,
						departure_time = (DateTime)fl.DepartureTime,
						arrival_time = (DateTime)fl.ArrivalTime,
						airline_id = fl.AirlineId,
						status = "-"
					};

				flightInfoList = await flightInfo.ToListAsync();

				_flightStatus.CheckStatus(flightInfoList);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error:" + e.Message);
			}

			return flightInfoList;
		}

	}
	 
}
