using FlightTaskProject.Controller;
using FlightTaskProject.DataContext;
using FlightTaskProject.Extensions.CheckHelpers;
using FlightTaskProject.Extensions.CreateCSVFile;
using FlightTaskProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

string connectionString = "Host=localhost;Database=FlightTaskDB;Username=postgres;Password=1234";

var services = new ServiceCollection();
services.AddSingleton<Application>();
services.AddSingleton<ICheckProvidedData, CheckProvidedData>();
services.AddSingleton<ICheckDataType, CheckDataType>();
services.AddSingleton<ICreateCSVFile, CreateCSVFile>();
services.AddSingleton<IFlight, Flight>();
services.AddSingleton<IFindFlight, FindFlight>();
services.AddSingleton<IFlightStatus, FlightStatus>();
services.AddDbContext<FlightTaskDbContext>(options => options.UseNpgsql(connectionString));

ServiceProvider serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetService<Application>();
service?.RunApp();
