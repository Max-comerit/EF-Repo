using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models;

public class Category
{
public int Id { get; set; }

[Column(TypeName = "nvarchar(50)")]
public string Name { get; set; } = string.Empty;
public int ManufacturerId { get; set; }
public Manufacturer Manufacturer { get; set; } = null!;
public ICollection<SubCategory>? SubCategories { get; set; }
}
