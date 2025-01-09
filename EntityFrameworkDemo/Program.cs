using System.Runtime.InteropServices;
using EntityFrameworkDemo.Data;
using EntityFrameworkDemo.Models;
using Microsoft.EntityFrameworkCore;

// var options = new DbContextOptionsBuilder<StudentDbContext>()
// .UseSqlite("Data Source = StudentDb.db")
// .Options; 

var factory = new StudentContextFactory();
using(var context = factory.CreateDbContext(args)){

// if (!context.Students.Any()){
//     context.Students.Add(new Student { Name = "Frank", Email = "Frank@gmail.com"});
//     context.Students.Add(new Student { Name = "Max", Email = "Max@gmail.com"});
//     context.SaveChanges();
//     Console.WriteLine("Databasen är uppdaterad");
// }



if (!context.Schools.Any()){
    Console.WriteLine ("Inga skolor registrerade, registrerar en första");
    Console.WriteLine ("Ange skolans namn");
    string newSchoolName = Console.ReadLine();
    Console.WriteLine ("Ange skolans address");
    string newSchoolAddress = Console.ReadLine();
    context.Schools.Add(new School { Name = newSchoolName, Adress = newSchoolAddress});
    context.SaveChanges();
    Console.WriteLine ($"Skolan: {newSchoolName} med addressen {newSchoolAddress} har lagts till");
} else {
    Console.WriteLine ("Följande skolor finns registrerade:");
    var schools = context.Schools.ToList();
    schools.ForEach(s => Console.WriteLine($"Skolnamn: {s.Name}, och adressen är: {s.Adress}"));
}

if (!context.Students.Any()){
    Console.WriteLine ("Inga elever registrerade");
// } else {
//     Console.WriteLine ("Hämtar elever från DB:n \n");
//     var students = context.Students.ToList();
//     students.ForEach(s => Console.WriteLine($"Student namn is: {s.Name}, and email is: {s.Email}"));
// };

// var specificSchool = context.Schools
//                         .Where(s => s.Name == "Newton" && s.Adress == "Frihamnen");
// var schoolNewton= specificSchool.FirstOrDefault();

// var specificStudent = context.Students
//                         .Where(s => s.Name == "Max");
// var studentMax= specificStudent.FirstOrDefault();
// studentMax.SchoolId = schoolNewton.Id;
// context.SaveChanges();
}

var studentToRemove = context.Students.FirstOrDefault();
context.Students.Remove(studentToRemove);
context.SaveChanges();

var updatedList = context.Students
                        .Include(s => s.School)
                        .ToList();
updatedList.ForEach(s => Console.WriteLine($"Student namn is: {s.Name}, and email is: {s.Email}, and school: {s.School.Name}"));
}
