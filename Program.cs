using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

public static class Program
{
    private struct DatabaseObject
    {
        public string name, author;
        public int price;
    }

    public static void Main()
    {
        DatabaseObject databaseObject;
        const string connectionString = @"Data Source=VOLODYA; Initial Catalog=BookShop; Integrated Security=True";

        Console.WriteLine("1 - Show | 2 - Insert");

        if (Convert.ToInt32(Console.ReadLine()) == 1)
        {
            Show(connectionString);
        }
        else
        {
            Console.WriteLine("Enter : name/author/price");

            databaseObject.name = Console.ReadLine();
            databaseObject.author = Console.ReadLine();
            databaseObject.price = Convert.ToInt32(Console.ReadLine());

            Insert(connectionString, databaseObject);
        }
    }

    private static void Show(string connectionString)
    {
        SqlConnection connection = null;
        const string commandString = "SELECT * FROM Book";

        try
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(commandString, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)}\t{reader.GetString(1)}\t{reader.GetString(2)}\t{reader.GetInt32(3)}");
                }
            }

            reader.Close();
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    private static void Insert(string connectionString, DatabaseObject databaseObject)
    {
        SqlConnection connection = null;
        const string commandString = "INSERT INTO Book(name, author, price) VALUES(@name, @author, @price)";

        try
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(commandString, connection);

            command.Parameters.Add(new SqlParameter("@name", databaseObject.name));
            command.Parameters.Add(new SqlParameter("@author", databaseObject.author));
            command.Parameters.Add(new SqlParameter("@price", databaseObject.price));

            command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }
}