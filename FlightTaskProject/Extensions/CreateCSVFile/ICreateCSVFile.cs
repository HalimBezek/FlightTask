using System.Collections;
using FlightTaskProject.Business;
using FlightTaskProject.Business.Model;

namespace FlightTaskProject.Extensions.CreateCSVFile
{
    public interface ICreateCSVFile
    {
        public Task CreateFile(List<ResultModel>? data);
    }
}
