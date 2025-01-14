using System;
using System.Reflection.Metadata.Ecma335;
using EntityFrameworkDemo.Data;
using EntityFrameworkDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using EntityFrameworkDemo.Services;
namespace EntityFrameworkDemo.Services;

public class DatabaseService
{
    public static void RunDbService()
    {
        Console.WriteLine("Welcome to the CRUD App");
        string command = string.Empty;

        while (command != "exit")
        {
            Console.WriteLine("\nAvailable commands:");
            Console.WriteLine("1: Create Entity");
            Console.WriteLine("2: Read Entities");
            Console.WriteLine("3: Update Entity");
            Console.WriteLine("4: Delete Entity");
            Console.WriteLine("Type 'exit' to close the application");
            Console.Write("\nEnter your command: ");
            command = Console.ReadLine();

            Console.Write("~~~~~ Input recieved ~~~~~\n");

            switch (command)
            {
                case "1":
                    CreateEntity();
                    break;
                case "2":
                    Console.WriteLine("\nSelect an alternative:");
                    Console.WriteLine("1: View a single entity");
                    Console.WriteLine("2: View a list of entities");
                    int choice = int.Parse(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                ReadEntities();
                                break;
                            case 2:
                                ReadAllEntities();
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    break;
                case "3":
                    UpdateEntity();
                    break;
                case "4":
                    DeleteEntity();
                    break;
                case "exit":
                    Console.WriteLine("Exiting application...");
                    break;
                default:
                    Console.WriteLine("Invalid command. Try again.");
                    break;
            }
        }
    }

    static void CreateEntity()
    {
        InputOutputService.PresentMeny();
        string choice = Console.ReadLine();
        Console.WriteLine("~~~~~ Accessing DB ~~~~~");

        using (var context = new ProductDbContext())
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Enter Manufacturer Name: ");
                    string manufacturerName = Console.ReadLine();
                    context.Manufacturers.Add(new Manufacturer { Name = manufacturerName });
                    break;

                case "2":
                    Console.Write("Enter Category Name: ");
                    string categoryName = Console.ReadLine();
                    Console.Write("Enter Manufacturer ID: ");
                    int manufacturerId = int.Parse(Console.ReadLine());
                    context.Categories.Add(new Category { Name = categoryName, ManufacturerId = manufacturerId });
                    break;

                case "3":
                    Console.Write("Enter Subcategory Name: ");
                    string subcategoryName = Console.ReadLine();
                    Console.Write("Enter Category ID: ");
                    int categoryId = int.Parse(Console.ReadLine());
                    context.SubCategories.Add(new SubCategory { Name = subcategoryName, CategoryId = categoryId });
                    break;

                case "4":
                    Console.Write("Enter Product Name: ");
                    string productName = Console.ReadLine();
                    Console.Write("Enter Subcategory ID: ");
                    int subcategoryId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Manufacturer ID");
                    var manufacturerIds = Console.ReadLine()?.Split(',').Select(int.Parse).ToList();
                    var product = new Product { Name = productName, SubCategoryId = subcategoryId };
                    context.Products.Add(product);
                    if (manufacturerIds != null)
                    {
                        foreach (var id in manufacturerIds)
                        {
                            var manufacturer = context.Manufacturers.Find(id);
                            if (manufacturer != null)
                                product.Manufacturer = manufacturer;
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            context.SaveChanges();
            Console.WriteLine("Entity created successfully!");
        }
        InputOutputService.EndOfEvent();
    }

    static void ReadEntities()
    {
        InputOutputService.PresentMeny();
        string choice = Console.ReadLine();
        Console.WriteLine("~~~~~ Accessing DB ~~~~~");

        using (var context = new ProductDbContext())
        {
            switch (choice)
            {
                case "1":
                    foreach (var manufacturer in context.Manufacturers)
                        Console.WriteLine($"ID: {manufacturer.Id}, Name: {manufacturer.Name}");
                    break;

                case "2":
                    foreach (var category in context.Categories.Include(c => c.Manufacturer))
                        Console.WriteLine($"ID: {category.Id}, Name: {category.Name}, Manufacturer: {category.Manufacturer.Name}");
                    break;

                case "3":
                    foreach (var subcategory in context.SubCategories.Include(s => s.Category))
                        Console.WriteLine($"ID: {subcategory.Id}, Name: {subcategory.Name}, Category: {subcategory.Category.Name}");
                    break;

                case "4":
                    foreach (var product in context.Products.Include(p => p.Manufacturer).Include(p => p.SubCategory))
                    {
                        Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Subcategory: {product.SubCategory.Name}, Manufacturer: {product.Manufacturer.Name}");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void ReadAllEntities()
    {
        InputOutputService.PresentMeny();
        string choice = Console.ReadLine();
        Console.WriteLine("~~~~~ Accessing DB ~~~~~");

    using (var context = new ProductDbContext())
    {
        switch (choice)
        {
            case "1": 
                Console.WriteLine("Manufacturers:");
                foreach (var manufacturer in context.Manufacturers)
                {
                    Console.WriteLine($"ID: {manufacturer.Id}, Name: {manufacturer.Name}");
                }
                break;

            case "2": 
                Console.WriteLine("Categories:");
                foreach (var category in context.Categories.Include(c => c.Manufacturer))
                {
                    Console.WriteLine($"ID: {category.Id}, Name: {category.Name}, Manufacturer: {category.Manufacturer?.Name ?? "None"}");
                }
                break;

            case "3": 
                Console.WriteLine("SubCategories:");
                foreach (var subcategory in context.SubCategories.Include(s => s.Category))
                {
                    Console.WriteLine($"ID: {subcategory.Id}, Name: {subcategory.Name}, Category: {subcategory.Category?.Name ?? "None"}");
                }
                break;

            case "4": 
                Console.WriteLine("Products:");
                foreach (var product in context.Products.Include(p => p.Manufacturer).Include(p => p.SubCategory))
                {
                    Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Subcategory: {product.SubCategory?.Name ?? "None"}, Manufacturer: {product.Manufacturer?.Name ?? "None"}");
                }
                break;

            default: 
                Console.WriteLine("Invalid choice, please select a valid option");
                return;
        }
        InputOutputService.EndOfEvent();
        }
    }

    static void UpdateEntity()
    {
        InputOutputService.PresentMeny();
        string choice = Console.ReadLine();
        Console.WriteLine("~~~~~ Accessing DB ~~~~~");

        using (var context = new ProductDbContext())
        {
            Console.Write("Enter ID of the entity to update: ");
            int id = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case "1":
                    var manufacturer = context.Manufacturers.Find(id);
                    if (manufacturer == null) { Console.WriteLine("Manufacturer not found."); return; }
                    Console.Write("Enter new Manufacturer Name: ");
                    manufacturer.Name = Console.ReadLine();
                    break;

                case "2":
                    var category = context.Categories.Find(id);
                    if (category == null) { Console.WriteLine("Category not found."); return; }
                    Console.Write("Enter new Category Name: ");
                    category.Name = Console.ReadLine();
                    break;

                case "3":
                    var subcategory = context.SubCategories.Find(id);
                    if (subcategory == null) { Console.WriteLine("Subcategory not found."); return; }
                    Console.Write("Enter new Subcategory Name: ");
                    subcategory.Name = Console.ReadLine();
                    break;

                case "4":
                    var product = context.Products.Include(p => p.Manufacturer).FirstOrDefault(p => p.Id == id);
                    if (product == null) { Console.WriteLine("Product not found"); return; }
                    Console.Write("Enter new Product Name: ");
                    product.Name = Console.ReadLine();
                    Console.WriteLine("Enter Manufacturer ID to update: ");
                    int manufacturerId = int.Parse(Console.ReadLine());
                    if (manufacturerId != null)
                    {
                    var manufacturerToAdd = context.Manufacturers.Find(manufacturerId);
                        if (manufacturerToAdd != null)
                    product.Manufacturer = manufacturerToAdd;
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    return;
            }
            context.SaveChanges();
            Console.WriteLine("Entity updated successfully");
            InputOutputService.EndOfEvent();
       }
    }

    static void DeleteEntity()
    {
        InputOutputService.PresentMeny();
        string choice = Console.ReadLine();
        Console.WriteLine("~~~~~ Accessing DB ~~~~~");

        using (var context = new ProductDbContext())
        {
            Console.Write("Enter ID of the entity to delete: ");
            int id = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case "1":
                    var manufacturer = context.Manufacturers.Find(id);
                    if (manufacturer != null) context.Manufacturers.Remove(manufacturer);
                    else Console.WriteLine("Manufacturer not found");
                    break;

                case "2":
                    var category = context.Categories.Find(id);
                    if (category != null) context.Categories.Remove(category);
                    else Console.WriteLine("Category not found");
                    break;

                case "3":
                    var subcategory = context.SubCategories.Find(id);
                    if (subcategory != null) context.SubCategories.Remove(subcategory);
                    else Console.WriteLine("Subcategory not found");
                    break;

                case "4":
                    var product = context.Products.Include(p => p.Manufacturer).FirstOrDefault(p => p.Id == id);
                    if (product != null) context.Products.Remove(product);
                    else Console.WriteLine("Product not found");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            context.SaveChanges();
            Console.WriteLine("Entity deleted successfully!");
            InputOutputService.EndOfEvent();
        }
    }
}
