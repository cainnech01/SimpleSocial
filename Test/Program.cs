using System.Data.SqlClient;

var connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=FTsR;Integrated Security=True;Connect Timeout=30;Encrypt=False";


SqlConnection connection = new SqlConnection(connectionString);

Console.WriteLine(connection.State);