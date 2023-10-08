namespace FlightTaskProject.Extensions.CheckHelpers
{
	public class CheckProvidedData: ICheckProvidedData
	{
        private readonly ICheckDataType _checkDataType;

        public CheckProvidedData(ICheckDataType checkDataType)
        {
			_checkDataType = checkDataType;
		}

        /// <summary>
        /// Check provided data for all exceptional typing
        /// </summary>
        /// <param name="provided"></param>
        /// <returns></returns>
        public bool CheckAndResult(string? provided)
        { 
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

            if (!_checkDataType.CheckDate(provided.Split(" ")[0], out DateTime? startDate))
            {
                Console.WriteLine("Start date format should be yyyy-MM-dd.");
                return false;
            }

            if (!_checkDataType.CheckDate(provided.Split(" ")[1], out DateTime? endDate))
            {
                Console.WriteLine("End date format should be yyyy-MM-dd.");
                return false;
            }

            if (!_checkDataType.CheckInt(provided.Split(" ")[2], out int? agencyId))
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
