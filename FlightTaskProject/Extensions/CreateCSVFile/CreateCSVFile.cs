using CsvHelper;
using System.Collections;
using System.Globalization;
using System.IO;

namespace FlightTaskProject.Extensions.CreateCSVFile
{
	internal class CreateCSVFile: ICreateCSVFile
	{
		private readonly IEnumerable _data;
		public CreateCSVFile(IEnumerable data)
		{
			_data = data;
		}

		/// <summary>
		/// Create CSV File from data
		/// </summary>
		/// <returns></returns>
		public async Task CreateFile()
		{
			string path = $"flight_{DateTime.Now:yyyy-MM-dd_hh-mm-ss}.csv";
			string folder = @"C:\FlightTask\CSVFiles\";

			var fullPath = Path.Combine(path, folder);
			bool exists = System.IO.Directory.Exists(fullPath); 
			if (!exists) Directory.CreateDirectory(folder); 

			await using var writer = new StreamWriter(folder + path);
			await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

			await csv.WriteRecordsAsync(_data);//todo: check data type.
		}
	}
}
