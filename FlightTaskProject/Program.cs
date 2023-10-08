using FlightTaskProject.DataContext;
using FlightTaskProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


try
{
	string connectionString = "Host=localhost;Database=FlightTaskDB;Username=postgres;Password=1234";
	var services = new ServiceCollection();
	services.AddSingleton<Application>();
	services.AddDbContext<FlightTaskDbContext>(options => options.UseNpgsql(connectionString));

	ServiceProvider serviceProvider = services.BuildServiceProvider();

	var service = serviceProvider.GetService<Application>();
	service?.RunApp();

}	
catch (Exception e)
{
	Console.WriteLine("something get wrong: " + e.Message);
}