using FlightTaskProject;


try
{
	Application.RunApp();
}	
catch (Exception e)
{
	Console.WriteLine("something get wrong: " + e.Message);
}