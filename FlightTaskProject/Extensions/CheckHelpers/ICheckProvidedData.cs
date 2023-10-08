using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTaskProject.Extensions.CheckHelpers
{
	internal interface ICheckProvidedData
	{
		bool CheckAndResult(string? provided);
	}
}
