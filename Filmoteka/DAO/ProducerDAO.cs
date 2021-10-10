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
        public static List<Producer> GetProducersByMovieId(int movieId)
        {
            string sqlSelectProducersByMovieId = "select pro.ProducerId, per.FirstName, per.LastName, per.Gender, per.DateOfBirth, per.BirthPlace, per.Bio, per.Nickname, per.Image" +
             " from person per inner join producermediacontent pro on pro.ProducerId = per.PersonId where pro.ContentId =" + movieId + ";";

            List<Producer> producers = new();

            DataTable dataTable = DBUtil.ExecuteExtraction(sqlSelectProducersByMovieId);

            foreach(DataRow row in dataTable.Rows)
                producers.Add(new Producer(
                                       row.Field<int>("ProducerId"),
                                       row.Field<string>("FirstName"),
                                       row.Field<string>("LastName"),
                                       row.Field<string>("Gender"),
                                       row.Field<string>("DateOfBirth"),
                                       row.Field<string>("BirthPlace"),
                                       row.Field<string>("Bio"),
                                       row.Field<string>("Nickname")
                                   ));
            return producers;
        }


        public static List<Producer> GetProducers()
        {
            const string sqlCommand = "select * from producers";
            List<Producer> dataList = new();
            DataTable dataTable = DBUtil.ExecuteExtraction(sqlCommand);

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
