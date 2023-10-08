using FlightTaskProject.Business.Model;

namespace FlightTaskProject.Business
{
	public class FlightStatus
	{
		/// <summary>
		/// Check flight status info
		/// </summary>
		/// <param name="flightInfo">Flight Info list</param>
		public void CheckStatus(List<ResultModel>? flightInfo)
		{
			var data = flightInfo.GroupBy(model => model.airline_id);
			foreach (IGrouping<int, ResultModel> gr in data.ToList())
			{
				int key = gr.Key;
				IEnumerable<ResultModel> groupModel = gr.Select(model => model);

				SetStatus(flightInfo, groupModel);
			}
		}

		/// <summary>
		/// Set flight status info
		/// </summary>
		/// <param name="flightInfo">flight informatin</param>
		/// <param name="groupModel">Result model after group by</param>
		private void SetStatus(List<ResultModel>? flightInfo, IEnumerable<ResultModel> groupModel)
		{
			int i = 0;
			List<ResultModel> groupList = groupModel.ToList();
			foreach (ResultModel model in groupList)
			{
				if (i == 0)//base of query dates, maybe before this query start date can be different
				{
					flightInfo.Where(m => m.flight_id == model.flight_id).ToList().ForEach(s => s.status = "New");
					i++;
					continue;
				}

				SetNewStatus(flightInfo, groupList, model);
				SetDiscontinuedStatus(flightInfo, groupList, model);

				i++;
				if (i > groupList.Count()) break;
			}
		}

		/// <summary>
		/// Set new status info
		/// </summary>
		/// <param name="flightInfo"></param>
		/// <param name="groupList">Model list after group by</param>
		/// <param name="model">ResultModel from forEach</param>
		private void SetNewStatus(List<ResultModel> flightInfo, List<ResultModel> groupList, ResultModel model)
		{
			var newDateStart = model.departure_time.AddDays(-7).AddMinutes(-30);
			var newDateEnd = model.departure_time.AddDays(-7).AddMinutes(+30);
			if (groupList.FirstOrDefault(a => a.departure_time > newDateStart && a.departure_time < newDateEnd) == null)
				flightInfo.Where(m => m.flight_id == model.flight_id).ToList().ForEach(s => s.status = "New");
		}

		/// <summary>
		/// Set Discontinued status info
		/// </summary>
		/// <param name="flightInfo">Flight info list</param>
		/// <param name="groupList">Model list after group by</param>
		/// <param name="model">ResultModel from forEach</param>
		private void SetDiscontinuedStatus(List<ResultModel> flightInfo, List<ResultModel> groupList, ResultModel model)
		{
			var discontinuedDateStart = model.departure_time.AddDays(+7).AddMinutes(-30);
			var discontinuedDateEnd = model.departure_time.AddDays(+7).AddMinutes(+30);
			if (groupList.FirstOrDefault(a => a.departure_time > discontinuedDateStart && a.departure_time < discontinuedDateEnd) == null)
				flightInfo.Where(m => m.flight_id == model.flight_id).ToList().ForEach(s => s.status = "Discontinued");

		}

	}
}
