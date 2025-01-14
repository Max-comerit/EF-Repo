using System;

namespace EntityFrameworkDemo.Services;

public class InputOutputService
{
        public static void PresentMeny(){
        Console.WriteLine("\nChoose an entity you wish to view:");
        Console.WriteLine("1: Manufacturers");
        Console.WriteLine("2: Categories");
        Console.WriteLine("3: Subcategories");
        Console.WriteLine("4: Products");
        Console.Write("Your choice: ");
}
     public static void EndOfEvent(){
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
    }
}
