namespace FlightTaskProject.Extensions.CheckHelpers
{
    internal class CheckDataType: ICheckDataType
	{
        /// <summary>
        /// Check int data type
        /// </summary>
        /// <param name="id">provided id </param>
        /// <param name="result">result data</param>
        /// <returns></returns>
        public bool CheckInt(string id, out int? result)
        {
            result = null;

            if (!int.TryParse(id, out int r))
                return false;

            result = r;

            return true;

        }

        /// <summary>
        /// Check DateTime data type
        /// </summary>
        /// <param name="date">provided date</param>
        /// <param name="result">result data</param>
        /// <returns></returns>
        public bool CheckDate(string date, out DateTime? result)
        {
            result = null;

            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime dt))
                return false;

            result = dt;

            return true;
        }
    }
}
