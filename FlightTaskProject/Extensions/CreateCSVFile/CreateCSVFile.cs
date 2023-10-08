using CsvHelper;
using System.Globalization;
using FlightTaskProject.Controller.Model;

namespace FlightTaskProject.Extensions.CreateCSVFile
{
    internal class CreateCSVFile : ICreateCSVFile
    {
        /// <summary>
        /// Create CSV File from data
        /// </summary>
        /// <returns></returns>
        public async Task CreateFile(List<ResultModel>? data)
        {
            string path = $"flight_{DateTime.Now:yyyy-MM-dd_hh-mm-ss}.csv";
            string folder = @"C:\FlightTask\CSVFiles\";

            var fullPath = Path.Combine(path, folder);
            bool exists = Directory.Exists(fullPath);
            if (!exists) Directory.CreateDirectory(folder);

            await using var writer = new StreamWriter(folder + path);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
           
            await csv.WriteRecordsAsync(data);
        }
    }
}
