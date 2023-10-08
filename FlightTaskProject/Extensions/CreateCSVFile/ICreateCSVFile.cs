using System.Collections;

namespace FlightTaskProject.Extensions.CreateCSVFile
{
    public interface ICreateCSVFile
    {
        public Task CreateFile(IEnumerable data);
    }
}
