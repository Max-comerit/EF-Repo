using EntityFrameworkDemo.Data;
using EntityFrameworkDemo.Services;

Console.Clear();
Console.WriteLine("Welcome to the App, what would you like to do?\n1: Access DB\n2: Exit application");

string response = Console.ReadLine();
int selectedNumber = Convert.ToInt32(response);

switch(selectedNumber) 
{
  case 1:
DatabaseService.RunDbService();
    break;
  case 2:
Console.WriteLine("Exiting application");
    break;
  default:
Console.WriteLine("No valid input was given, application exits");
    break;
}