using Microsoft.Data.Sqlite;
using students_ef;

SqliteConnectionStringBuilder csb = new() { DataSource = "database.db" };
string connectionString = csb.ConnectionString;

Student s = new(1, "firstname", "lastname");
s.Print();
