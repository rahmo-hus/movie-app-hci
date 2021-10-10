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

        public static List<Genre> GetGenresByMovieId(int movieId)
        {
            string sqlSelectGenresByMovieId = "select g.genreId, g.CategoryName from genre g " +
                "inner join genremediacontent gmc on gmc.GenreId = g.GenreId where gmc.ContentId =" + movieId + ";";

            List<Genre> genres = new();
            DataTable dataTable = DBUtil.ExecuteExtraction(sqlSelectGenresByMovieId);

            foreach(DataRow row in dataTable.Rows)
                genres.Add(new Genre(row.Field<int>("GenreId"), row["CategoryName"].ToString()));

            return genres;
        }

        public static List<Genre> GetGenres()
        {
            const string sqlCommand = "select * from genres";
            List<Genre> dataList = new();
            DataTable table = DBUtil.ExecuteExtraction(sqlCommand);

            foreach (DataRow row in table.Rows)
                dataList.Add(new Genre(row.Field<int>("GenreId"), row["CategoryName"].ToString()));

            return dataList;
        }
    }
}
