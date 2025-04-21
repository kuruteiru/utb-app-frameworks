using Microsoft.EntityFrameworkCore;
namespace students_ef;

public class Student
{
	public int ID { get; init; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
	public required bool Active { get; set; }

	public void Print()
	{
		Console.WriteLine($"id: {this.ID}");
		Console.WriteLine($"first name: {this.FirstName}");
		Console.WriteLine($"last name: {this.LastName}");
		Console.WriteLine($"active: {this.Active}");
	}

	public static void PrintStudents(IEnumerable<Student> students)
	{
		Console.WriteLine("students:");
		foreach (Student student in students)
			Console.WriteLine($"{student.ID}: {student.FirstName} {student.LastName}, active: {student.Active}");
	}
}

public class StudentContext(DbContextOptions<StudentContext> options) : DbContext(options)
{
	public DbSet<Student> Students { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Student>().HasData(
			new Student { ID = 1, FirstName = "hynek", LastName = "studeny", Active = false },
			new Student { ID = 2, FirstName = "jiri", LastName = "gabon", Active = true },
			new Student { ID = 3, FirstName = "dj", LastName = "terenz", Active = false }
		);
	}
}
