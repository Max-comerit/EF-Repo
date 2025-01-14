using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models;

public class Manufacturer
{
public int Id { get; set; }

[Column(TypeName = "nvarchar(50)")]
public string Name { get; set; } = string.Empty;

[Column(TypeName = "nvarchar(100)")]
public string Adress { get; set; } = string.Empty;
public ICollection<Category>? Categories { get; set; }

public ICollection<Product>? Products { get; set; }

}
