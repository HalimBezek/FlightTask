using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTaskProject.Helpers;

namespace FlightTaskProject
{
	public static class Application
	{
		public static void RunApp()
		{
			BuildApp();
		}

		private static void BuildApp()
		{
			CheckProvidedData check = new CheckProvidedData();//todo: note : can be static or dpInjection, refactor

			Console.WriteLine("Please provide date ranges and agency id");
			Console.WriteLine("Should be yyyy-MM-dd yyyy-MM-dd id in order");
			string? provided = Console.ReadLine();

			while (!check.CheckAndResult(provided))
			{
			    provided = Console.ReadLine();
			} 

			Console.WriteLine(provided);
		}
		
	
	}
}
