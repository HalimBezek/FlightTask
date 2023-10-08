using FlightTaskProject.Business;
using FlightTaskProject.Controller.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTaskProject.Controller
{
    public interface IFlight
	{
		public Task Run(QueryModel queryParam);
	}
}
