﻿namespace FlightTaskProject.Business.Model
{
	public class ResultModel
	{
		public int flight_id { get; set; }
		public int origin_city_id { get; set; }
		public int destination_city_id { get; set; }
		public DateTime departure_time { get; set; }
		public DateTime arrival_time { get; set; }
		public int airline_id { get; set; }
		public string status { get; set; }
	}
}
