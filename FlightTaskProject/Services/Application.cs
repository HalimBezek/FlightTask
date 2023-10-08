using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTaskProject.Business;
using FlightTaskProject.DataContext;
using FlightTaskProject.Extensions.CheckHelpers;
using Microsoft.EntityFrameworkCore;

namespace FlightTaskProject.Services
{
    public class Application
    {
        private FlightTaskDbContext _context;

        public Application(FlightTaskDbContext context)
        {
            _context = context;

        }
        public void RunApp()
        {
            BuildApp();
        }

        private void BuildApp()
        {
            CheckProvidedData check = new CheckProvidedData();//todo: note : can be static or dpInjection, refactor

			Console.WriteLine("Please provide date ranges and agency id");
            Console.WriteLine("Should be yyyy-MM-dd yyyy-MM-dd id in order");
            string? provided = Console.ReadLine();

            while (!check.CheckAndResult(provided))
            {
                provided = Console.ReadLine();
            }

            QueryParam parapm = FillModel(provided);

            CheckFlight cc = new CheckFlight(_context);//todo: note : can be static or dpInjection, refactor
			cc.CreateData(parapm).Wait();
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
