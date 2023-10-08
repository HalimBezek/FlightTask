using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTaskProject.Extensions.CheckHelpers
{
	internal interface ICheckDataType
	{
		bool CheckInt(string id, out int? result);
		bool CheckDate(string date, out DateTime? result);
	}
}
