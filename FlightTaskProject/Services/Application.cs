using System.Globalization;
using FlightTaskProject.Business;
using FlightTaskProject.Controller;
using FlightTaskProject.Controller.Model;
using FlightTaskProject.Extensions.CheckHelpers;

namespace FlightTaskProject.Services
{
    public class Application
    {
        private readonly IFlight _flight;
        private readonly ICheckProvidedData _checkProvidedData;

        public Application(IFlight flight, ICheckProvidedData checkProvidedData)
        {
	        _flight = flight;
            _checkProvidedData = checkProvidedData;
		}
        public void RunApp()
        {
            BuildApp();
        }

        private void BuildApp()
        {
			Console.WriteLine("Please provide date ranges and agency id \nShould be yyyy-MM-dd yyyy-MM-dd id in order");
            string? provided = Console.ReadLine();

            while (!_checkProvidedData.CheckAndResult(provided))
            {
                provided = Console.ReadLine();
            }

            QueryModel param = FillModel(provided);

            _flight.Run(param).Wait();
        }

        /// <summary>
        /// fill query model from provided data
        /// </summary>
        /// <param name="provided">provided data</param>
        /// <returns></returns>
        private QueryModel FillModel(string provided)
        {
            QueryModel param = new QueryModel(new QueryResultModel(new List<int>(),new List<int?>()));

            param.StartDate = DateTime.ParseExact(provided.Split(" ")[0], "yyyy-MM-dd", CultureInfo.InvariantCulture); 
            param.EndDate = DateTime.ParseExact(provided.Split(" ")[1], "yyyy-MM-dd", CultureInfo.InvariantCulture);
			param.AgencyId = Convert.ToInt32(provided.Split(" ")[2]);

            return param;
        }
    }
}
