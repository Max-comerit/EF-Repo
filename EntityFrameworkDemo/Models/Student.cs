using System;

namespace EntityFrameworkDemo.Models;

public class Student
{
public int Id { get; set; }
public string? Name { get; set; }
public string? Email { get; set; }

public int? SchoolId { get; set; }
public School? School { get; set; }
}
