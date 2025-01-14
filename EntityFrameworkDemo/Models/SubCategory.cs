using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkDemo.Models;

public class SubCategory
{
public int Id { get; set; }

[Column(TypeName = "nvarchar(50)")]
public string Name { get; set; } = string.Empty;

[Column(TypeName = "nvarchar(100)")]
public string Description { get; set; } = string.Empty;
public int CategoryId { get; set; }

[Required]
public Category Category { get; set; } = null!;

public ICollection<Product>? Products { get; set; }
}


