using FlightTaskProject;


Console.WriteLine("Hello, World!");

try
{
	Application.RunApp();
}	
catch (Exception e)
{
	Console.WriteLine("something get wrong: " + e.Message);
}