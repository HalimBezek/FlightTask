namespace FlightTaskProject.Extensions.CheckHelpers
{
	public interface ICheckDataType
	{
		bool CheckInt(string id, out int? result);
		bool CheckDate(string date, out DateTime? result);
	}
}
