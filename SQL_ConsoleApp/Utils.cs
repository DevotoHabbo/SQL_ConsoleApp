using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;


namespace SQL_ConsoleApp
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
    class Utils
    {
        public static void SQL_Console(out SqlCeConnection connection, out SqlCeCommand command)
        {

            // File name
            string dbFile = "Person.sdf";
            string connectionString = $"Data Source={dbFile}";
            connection = new SqlCeConnection(connectionString);
            command = connection.CreateCommand();

            WriteInSQL(connection, command);

            // Init while loop which allow the user to add as much data as they want to as long as they choose "y" as answer;

            // Init the connection to the database
            SqlCeDataReader reader = ReadSQL(connection, command);

        }

        private static SqlCeDataReader ReadSQL(SqlCeConnection connection, SqlCeCommand command)
        {
            connection.Open();
            //Read the data from the database
            command.CommandText = "SELECT * FROM Person";
            SqlCeDataReader reader = command.ExecuteReader();


            List<Person> persons = new List<Person>();
            while (reader.Read())
            {
                Person sql = new Person();
                sql.FirstName = (string)reader[1];
                sql.LastName = (string)reader[2];
                persons.Add(sql);
            }
            foreach (var person in persons)
            {
                Console.WriteLine("Firstname:{0}" + " " + "Lastname:{1}", person.FirstName, person.LastName);

            }

            return reader;
        }

        private static void WriteInSQL(SqlCeConnection connection, SqlCeCommand command)
        {
            Console.WriteLine("What is your firstname and lastname??");
            Person person = new Person();
            person.FirstName = Console.ReadLine();
            person.LastName = Console.ReadLine();
            // Insert the data into firstname and lastname column with the saved values from the Person constructor
            command.CommandText = "INSERT INTO Person(firstname,lastname)" + "VALUES ('" + person.FirstName + "','" + person.LastName + "') ";
            // Open the Query and run the execution
            connection.Open();
            command.ExecuteNonQuery();
            // Close the Query to save the data into the database
            connection.Close();
        }
                   
    }
}
