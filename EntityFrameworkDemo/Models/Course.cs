using System;

namespace EntityFrameworkDemo.Models;

public class Course
{
public int Id { get; set; }
public string? Name { get; set; }
public int Year { get; set; }

public int SchoolId { get; set; }
public School School { get; set; } = null!;
}
