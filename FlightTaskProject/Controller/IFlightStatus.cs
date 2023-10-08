using FlightTaskProject.Controller.Model;

namespace FlightTaskProject.Controller
{
	public interface IFlightStatus
	{
		public void CheckStatus(List<ResultModel>? flightInfo);
		public void SetStatus(List<ResultModel>? flightInfo, IEnumerable<ResultModel> groupModel);
		public void SetNewStatus(List<ResultModel> flightInfo, List<ResultModel> groupList, ResultModel model);
		public void SetDiscontinuedStatus(List<ResultModel> flightInfo, List<ResultModel> groupList, ResultModel model);
	}
}
