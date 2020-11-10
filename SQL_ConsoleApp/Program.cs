using System.Data.SqlServerCe;

namespace SQL_ConsoleApp
{
    class Program //Author: Phuong Nguyen, Date :11/09/2020
    {
        static void Main(string[] args)
        {
            SqlCeConnection connection;
            SqlCeCommand command;
            Utils.SQL_Console(out connection, out command);
        }
    }
}
