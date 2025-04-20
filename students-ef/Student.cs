using Microsoft.EntityFrameworkCore;

namespace students_ef;

public class Student(int id, string firstName, string lastName, bool active = true)
{
	public int ID { get; init; } = id;
	public string FirstName { get; set; } = firstName;
	public string LastName { get; set; } = lastName;
	public bool Active {get; set;} = active;

	public void Print()
	{
		Console.WriteLine($"id: {this.ID}");
		Console.WriteLine($"first name: {this.FirstName}");
		Console.WriteLine($"last name: {this.LastName}");
		Console.WriteLine($"active: {this.Active}");
	}
}

public class StudentContext : DbContext
{
	public DbSet<Student> Students { get; set; }
}
