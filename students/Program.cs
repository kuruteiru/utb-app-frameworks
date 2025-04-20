using Microsoft.Data.Sqlite;

SqliteConnectionStringBuilder csb = new() { DataSource = "database.db" };
string connectionString = csb.ConnectionString;

CreateDB();
AddStudent(1, "hanys", 10);
AddStudent(2, "gabu", 2);
AddStudent(3, "jesus", 999);
PrintStudents();

void CreateDB()
{
	using (SqliteConnection connection = new(connectionString))
	{
		connection.Open();
		SqliteTransaction transaction = connection.BeginTransaction();
		SqliteCommand command = connection.CreateCommand();
		command.Transaction = transaction;

		try
		{
			command.CommandText = @"
				CREATE TABLE IF NOT EXISTS students
				(
					id INTEGER PRIMARY KEY, 
					name TEXT,
					credits INTEGER
				)
			";
			command.ExecuteNonQuery();

			transaction.Commit();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			transaction.Rollback();
		}
	}
}

void AddStudent(int id, string name, int credits)
{
	using (SqliteConnection connection = new(connectionString))
	{
		connection.Open();
		using (SqliteCommand command = connection.CreateCommand())
		{
			command.CommandText = @"
				INSERT INTO students (name, credits)
				VALUES (@name, @credits)
			";

			command.Parameters.AddWithValue("@name", name);
			command.Parameters.AddWithValue("@credits", credits);

			int affectedRows = command.ExecuteNonQuery();
		}
	}
}

List<Student> SelectStudents()
{
	List<Student> students = new();
	using (SqliteConnection connection = new(connectionString))
	{
		connection.Open();
		using (SqliteCommand command = connection.CreateCommand())
		{
			command.CommandText = "SELECT * FROM students";
			SqliteDataReader reader = command.ExecuteReader();

			if (!reader.HasRows) return students;

			while (reader.Read())
			{
				Student student = new(
					id: reader.GetInt32(reader.GetOrdinal("id")),
					name: reader.GetString(reader.GetOrdinal("name")),
					credits: reader.GetInt32(reader.GetOrdinal("credits"))
				);
				students.Add(student);
			}
		}
	}

	return students;
}

void PrintStudents()
{
	foreach (Student student in SelectStudents())
	{
		student.Print();
		Console.WriteLine();
	}
}

struct Student
{
	public int id { get; set; }
	public string name { get; set; }
	public int credits { get; set; }

	public Student(int id, string name, int credits)
	{
		this.id = id;
		this.name = name;
		this.credits = credits;
	}

	public void Print()
	{
		Console.WriteLine($"id: {this.id}");
		Console.WriteLine($"name: {this.name}");
		Console.WriteLine($"credits: {this.credits}");
	}
}
