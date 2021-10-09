using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Repository
{
    class ItemExtractor
    {
        public static readonly string connectionString = "Server=127.0.0.1;Database=mydb;Uid=root;Password=password;";

        public static DataTable ExecuteExtraction(string sqlCommand)
        {
            DataTable table = new();
            using MySqlConnection mySqlConnection = new(connectionString);
            MySqlCommand command = new(sqlCommand, mySqlConnection);
            mySqlConnection.Open();
            table.Load(command.ExecuteReader());
            mySqlConnection.Close();
            return table;
        }
    }
}
