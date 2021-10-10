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
    class StarDAO
    {

        public static List<Star> GetCastByMovieId(int movieId)
        {
            string sqlSelectCastByMovieId = "select s.starId, p.FirstName, p.LastName, p.Gender, p.DateOfBirth, p.BirthPlace, p.Bio, p.Nickname from person p" +
            " inner join starmediacontent s on s.StarId = p.PersonId where s.ContentId = " + movieId + ";";

            List<Star> stars = new();

            DataTable dataTable = DBUtil.ExecuteExtraction(sqlSelectCastByMovieId);

            foreach (DataRow row in dataTable.Rows)
                stars.Add(new Star(
                    row.Field<int>("StarId"),
                    row.Field<string>("FirstName"),
                    row.Field<string>("LastName"),
                    row.Field<string>("Gender"),
                    row.Field<string>("DateOfBirth"),
                    row.Field<string>("BirthPlace"),
                    row.Field<string>("Bio"),
                    row.Field<string>("Nickname")
                ));

            return stars;
        }

        public static List<Star> GetCast()
        {
            const string sqlCommand = "select * from stars";
            List<Star> dataList = new();
            DataTable dataTable = DBUtil.ExecuteExtraction(sqlCommand);

            foreach (DataRow row in dataTable.Rows)
                dataList.Add(new Star(
                        row.Field<int>("StarId"),
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
