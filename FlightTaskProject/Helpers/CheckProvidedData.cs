using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTaskProject.Helpers
{
	internal class CheckProvidedData
	{
		private CheckDataType _checkData;

		/// <summary>
		/// Check provided data for all exceptional typing
		/// </summary>
		/// <param name="provided"></param>
		/// <returns></returns>
		public bool CheckAndResult(string? provided)
		{
			_checkData = new CheckDataType();//todo: can be static or dpInjection, refactor

			if (string.IsNullOrWhiteSpace(provided))
			{
				Console.WriteLine("Please provide date ranges and agency id.");
				return false;
			}

			if (provided.Trim().Split(" ").Length != 3)
			{
				Console.WriteLine("Please provide two date ranges and agency id.");
				return false;
			}

			if (!_checkData.CheckDate(provided.Split(" ")[0], out DateTime? startDate))
			{
				Console.WriteLine("Start date format should be yyyy-MM-dd.");
				return false;
			}

			if (!_checkData.CheckDate(provided.Split(" ")[1], out DateTime? endDate))
			{
				Console.WriteLine("End date format should be yyyy-MM-dd.");
				return false;
			}

			if (!_checkData.CheckInt(provided.Split(" ")[2], out int? agencyId))
			{
				Console.WriteLine("Agency Id should be a integer value.");
				return false;
			}

			Console.WriteLine("startDate :" + startDate);
			Console.WriteLine("endDate :" + endDate);
			Console.WriteLine("agencyId :" + agencyId);

			return true;
		}
	}
}
