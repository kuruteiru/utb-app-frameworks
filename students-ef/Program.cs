using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using students_ef;

public static class Program
{
	public const string DatabaseName = "database.db";

	public static void Main()
	{
		using StudentContext context = CreateContext();
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();

		Student student = new() { FirstName = "matyas", LastName = "havranina", Active = true };
		context.Add(student);
		if (context.SaveChanges() > 0)
			Console.WriteLine($"student {student.ID} pridan do db");

		Student.PrintStudents(context.Students.ToList());

		/*IQueryable<Student> students = context.Students.Where(s => s.Active == false);*/
		/*var students = context.Students.OrderBy(s => s.FirstName);*/
		/*context.Students.Find(2)?.Print();*/
		/*context.Students.FirstOrDefault(s => s.Active == false)?.Print();*/
		/*Student.PrintStudents(students);*/
		/*var names = context.Students.Select(s => s.FirstName);*/
		/*foreach (var name in names) Console.WriteLine($"name: {name}");*/
	}

	public static StudentContext CreateContext()
	{
		SqliteConnectionStringBuilder csb = new() { DataSource = DatabaseName };
		var options = new DbContextOptionsBuilder<StudentContext>()
			.UseSqlite(csb.ConnectionString)
			.Options;

		return new StudentContext(options);
	}
}
