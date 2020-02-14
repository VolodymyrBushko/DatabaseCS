using System;
using System.Data.SqlClient;

namespace ProjectCS
{
    class Program
    {
        static void Main(string[] args)
        {
            const string nameServer = "VOLODYA", nameDatabase = "MyDatabase";
            string connectionString = $"Data Source={nameServer}; Initial Catalog={nameDatabase}; Integrated Security=True";

            const string sqlCommandSelectUsers = "SELECT * FROM [User]";

            string login = Console.ReadLine();
            string password = Console.ReadLine();

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(sqlCommandSelectUsers, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (IsEnter(reader, login, password))
                    Console.WriteLine("Enter!");
                else
                    Console.WriteLine("Error!");
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

        static bool IsEnter(SqlDataReader reader, string login, string password)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (login.Equals(reader.GetString(0)) && password.Equals(reader.GetString(1)))
                        return true;
                }
            }
            return false;
        }
    }
}