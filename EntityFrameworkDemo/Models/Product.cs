using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models;

public class Product
{
public int Id { get; set; }

[Column(TypeName = "nvarchar(50)")]
public string Name { get; set; } = string.Empty;

[Column(TypeName = "nvarchar(100)")]
public string Description { get ; set; } = string.Empty;
public int SubCategoryId { get; set; }
public SubCategory SubCategory { get; set; } = null!;

public int ManufacturerId { get; set; }
public Manufacturer Manufacturer { get; set; } = null!;
}
