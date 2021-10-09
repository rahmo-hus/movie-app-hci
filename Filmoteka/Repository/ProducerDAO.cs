using Filmoteka.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Repository
{
    class ProducerDAO
    {
        public static readonly string connectionString = "Server=127.0.0.1;Database=mydb;Uid=root;Password=password;";

        private static DataTable ExecuteExtraction(string sqlCommand)
        {
            DataTable table = new();
            using MySqlConnection mySqlConnection = new(connectionString);
            MySqlCommand command = new(sqlCommand, mySqlConnection);
            mySqlConnection.Open();
            table.Load(command.ExecuteReader());
            mySqlConnection.Close();
            return table;
        }

        public static List<Producer> GetProducers()
        {
            const string sqlCommand = "select * from producers";
            List<Producer> dataList = new();
            DataTable dataTable = ExecuteExtraction(sqlCommand);

            foreach (DataRow row in dataTable.Rows)
                dataList.Add(new Producer(
                        row.Field<int>("ProducerId"),
                        row.Field<string>("FirstName"),
                        row.Field<string>("LastName"),
                        row.Field<string>("Gender"),
                        row.Field<string>("DateOfBirth"),
                        row.Field<string>("BirthPlace"),
                        row.Field<string>("Bio"),
                        row.Field<string>("Nickname")
                    ));

            return dataList;
        }
    }
}
