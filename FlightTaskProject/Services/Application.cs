using System.Globalization;
using FlightTaskProject.Business;
using FlightTaskProject.Extensions.CheckHelpers;

namespace FlightTaskProject.Services
{
    public class Application
    {
        private readonly CheckFlight _checkFlight;
        private readonly ICheckProvidedData _checkProvidedData;

        public Application(CheckFlight checkFlight, ICheckProvidedData checkProvidedData)
        {
	        _checkFlight = checkFlight;
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

            QueryParam param = FillModel(provided);

            _checkFlight.CreateData(param).Wait();
        }

        /// <summary>
        /// fill query model from provided data
        /// </summary>
        /// <param name="provided">provided data</param>
        /// <returns></returns>
        private QueryParam FillModel(string provided)
        {
            QueryParam param = new QueryParam();
            param.StartDate = DateTime.ParseExact(provided.Split(" ")[0], "yyyy-MM-dd", CultureInfo.InvariantCulture); 
            param.EndDate = DateTime.ParseExact(provided.Split(" ")[1], "yyyy-MM-dd", CultureInfo.InvariantCulture);
			param.AgencyId = Convert.ToInt32(provided.Split(" ")[2]);

            return param;
        }
    }
}
