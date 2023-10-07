using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTaskProject.Extensions.CreateCSVFile
{
	public interface ICreateCSVFile
	{
		public void CreateFile(DataSet data);
	}
}
