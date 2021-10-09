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
    class GenreDAO
    {

        public static List<Genre> GetGenres()
        {
            const string sqlCommand = "select * from genres";
            List<Genre> dataList = new();
            DataTable table = ItemExtractor.ExecuteExtraction(sqlCommand);

            foreach (DataRow row in table.Rows)
                dataList.Add(new Genre(row.Field<int>("GenreId"), row["CategoryName"].ToString()));

            return dataList;
        }
    }
}
