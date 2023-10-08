using FlightTaskProject.Controller.Model;

namespace FlightTaskProject.Extensions.CreateCSVFile
{
    public interface ICreateCSVFile
    {
        public Task CreateFile(List<ResultModel>? data);
    }
}
