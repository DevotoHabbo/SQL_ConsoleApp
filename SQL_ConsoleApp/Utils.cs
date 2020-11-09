using System;
using System.Data.SqlServerCe;


namespace SQL_ConsoleApp
{
    class Utils
    {
        public static void SQL_Console(out SqlCeConnection connection, out SqlCeCommand command)
        {
            // File name
            var dbFile = "Person.sdf";
            var connectionString = $"Data Source={dbFile}";
            connection = new SqlCeConnection(connectionString);
            connection.Open();
            command = connection.CreateCommand();

            // Init while loop which allow the user to add as much data as they want to as long as they choose "y" as answer;
            var adding = true;
            while (adding)
            {
                Console.WriteLine("Do you want to add a record? y/n");
                var answer = Console.ReadLine();
                if (answer == "y")
                {
                    Console.WriteLine("What is your firstname?");
                    var firstname = Console.ReadLine();
                    Console.WriteLine("What is your lastname?");
                    var lastname = Console.ReadLine();
                    command.CommandText = "INSERT INTO Person VALUES ('" + firstname + "','" + lastname + "') ";
                    command.ExecuteNonQuery();
                }
                else
                {
                    adding = false;

                    //Read the data from the database
                    command.CommandText = "SELECT * FROM Person";
                    //init SQl Server Compact's Read
                    using SqlCeDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"Firstname:{reader[0]} - Lastname:{reader[1]}");

                    }

                }

            };
        }
}
