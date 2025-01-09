using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkDemo.Models;

public class School
{
public int Id { get; set; }

public string? Name { get; set; }
public string? Adress { get; set; }
public int? Age { get; set; }
public ICollection<Student>? Students { get; set; }


}
