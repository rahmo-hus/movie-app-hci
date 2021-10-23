using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Filmoteka.DAO
{
    class DBUtil
    {
        public static readonly string connectionString = ConfigurationManager.AppSettings["connectionString"];

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

        public static int ExecuteCommand(string sqlCommand)
        {
            int numberOfRowsAffected;
            using MySqlConnection mySqlConnection = new(connectionString);
            MySqlCommand command = new(sqlCommand, mySqlConnection);
            mySqlConnection.Open();
            numberOfRowsAffected = command.ExecuteNonQuery();
            mySqlConnection.Close();
            return numberOfRowsAffected;
        }
    }
}
