using Filmoteka.Model;
using Filmoteka.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Filmoteka.Repository
{
    class MovieDAO
    {
        public static readonly string connectionString = ConfigurationManager.AppSettings["connectionString"];

        #region Get All Movies
        public static List<Movie> GetAllMovies()
        {
            const string sqlMovieDataStatement = "select mov.ContentId, med.Name, med.Description, med.Budget, med.CountryId, med.LanguageId,med.Image,  mov.Duration from mediacontent med " +
                "inner join movie mov on mov.ContentId = med.ContentId;";

            DataTable movieContentDataTable = DBUtil.ExecuteExtraction(sqlMovieDataStatement);

            List<Movie> movies = new();

            foreach(DataRow row in movieContentDataTable.Rows)
            {
                Movie localMovie = new();
                localMovie.ID = row.Field<int>("ContentId");
                localMovie.Name = row.Field<string>("Name");
                localMovie.Description = row.Field<string>("Description");
                localMovie.Duration = row.Field<int>("Duration");
                localMovie.Image = ImageUtil.ConvertByteArrToBitmap(row.Field<byte[]>("Image"));
                localMovie.OriginCountry = CountryDAO.GetCountryById(row.Field<int>("CountryId"));
                localMovie.Language = LanguageDAO.GetLanguageById(row.Field<int>("LanguageId"));
                localMovie.Producers = ProducerDAO.GetProducersByMovieId(localMovie.ID);
                localMovie.Stars = StarDAO.GetCastByMovieId(localMovie.ID);
                localMovie.Genres = GenreDAO.GetGenresByMovieId(localMovie.ID);
                movies.Add(localMovie);
            }
           
            return movies;
        }
        #endregion

        #region Delete movie

        public static bool Delete(int movieId)
        {
            using MySqlConnection connection = new(connectionString);
            connection.Open();
            MySqlCommand command = new(null, connection);
            command.CommandText = "delete_movie";
            command.CommandType = CommandType.StoredProcedure;

            MySqlParameter idParam = new("@movieId", MySqlDbType.Int32);
            idParam.Value = movieId;
            command.Parameters.Add(idParam);

            command.Prepare();
            int numberOfRowsAffected = command.ExecuteNonQuery();

            connection.Close();

            return numberOfRowsAffected != 0;
        }
        #endregion Delete movie

        #region Update movie
        public static Movie Update(Movie movie)
        {
            string sqlUpdateMovieData = "update mediacontent med inner join movie mov on mov.ContentId = med.ContentId set " +
                "med.Name = '"+movie.Name+"', med.Description ='"+movie.Description+"', med.CountryId = "+movie.OriginCountry.ID+", " +
                "med.LanguageId = "+movie.Language.ID+", mov.Duration = "+movie.Duration+", med.Budget = "+movie.Budget +
                " where mov.ContentId ="+movie.ID+";";
            //", med.Image = "+ConvertBitmapToByteArr(movie.Image)+"

            string sqlDeleteStatements = "delete from producermediacontent pmc where pmc.ContentId =" + movie.ID + ";" +
                "delete from starmediacontent smc where smc.ContentId =" + movie.ID + ";" +
                "delete from genremediacontent gmc where gmc.ContentId =" + movie.ID + ";";

            string updateProducers = "insert into producermediacontent values("+movie.ID+",?); ";
            string updateStars = "insert into starmediacontent values(" + movie.ID + ",?); ";
            string updateGenres ="insert into genremediacontent(ContentId, GenreId) values("+movie.ID+",?); ";

            DBUtil.ExecuteCommand(sqlUpdateMovieData);
            DBUtil.ExecuteCommand(sqlDeleteStatements);
            movie.Producers.ForEach(producer =>  DBUtil.ExecuteCommand(updateProducers.Replace('?', Char.Parse(producer.ID.ToString()))));
            movie.Stars.ForEach(star => DBUtil.ExecuteCommand(updateStars.Replace('?', Char.Parse(star.ID.ToString()))));
            movie.Genres.ForEach(genre => DBUtil.ExecuteCommand(updateGenres.Replace('?', Char.Parse(genre.ID.ToString()))));
            if (movie.Image != null)
            {
                using MySqlConnection mySqlConnection = new(ConfigurationManager.AppSettings["connectionString"]);
                MySqlCommand imageInsertCommand = new("update mediacontent set Image=@image where ContentId=" + movie.ID, mySqlConnection);
                mySqlConnection.Open();
                MySqlParameter imageParam = new("@image", MySqlDbType.LongBlob);
                imageParam.Value = ImageUtil.ConvertBitmapToByteArr(movie.Image);
                imageInsertCommand.Parameters.Add(imageParam);
                imageInsertCommand.Prepare();
                imageInsertCommand.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            return movie;
        }
        #endregion

        #region Save movie
        public static Movie Save(Movie movie)
        {
            using MySqlConnection connection = new(connectionString);
            connection.Open();
            MySqlCommand command = new(null, connection);
            command.CommandText = "insert_movie_data";
            command.CommandType = CommandType.StoredProcedure;
            MySqlParameter nameParam = new("@name", MySqlDbType.Text);
            MySqlParameter descriptionParam = new("@description", MySqlDbType.Text);
            MySqlParameter budgetParam = new("@budget", MySqlDbType.Float);
            MySqlParameter countryParam = new("@countryId", MySqlDbType.Int32);
            MySqlParameter languageParam = new("@languageId", MySqlDbType.Int32);
            MySqlParameter durationParam = new("@duration", MySqlDbType.Int32);
            MySqlParameter imageParam = new("@image", MySqlDbType.Blob);

            nameParam.Value = movie.Name;
            descriptionParam.Value = movie.Description;
            budgetParam.Value = movie.Budget;
            countryParam.Value = movie.OriginCountry.ID;
            languageParam.Value = movie.Language.ID;
            durationParam.Value = movie.Duration;
            imageParam.Value = ImageUtil.ConvertBitmapToByteArr(movie.Image);

            command.Parameters.Add(nameParam);
            command.Parameters.Add(descriptionParam);
            command.Parameters.Add(budgetParam);
            command.Parameters.Add(countryParam);
            command.Parameters.Add(languageParam);
            command.Parameters.Add(durationParam);
            command.Parameters.Add(imageParam);

            command.Prepare();
            movie.ID = Convert.ToInt32(command.ExecuteScalar());

            command.Parameters.Clear();
            command.CommandText = "insert_genre_into_movie";
            MySqlParameter genreIdParam = new("@genreId", MySqlDbType.Int32);
            MySqlParameter movieIdParam = new("@movieId", MySqlDbType.Int32);
            movieIdParam.Value = movie.ID;

            command.Parameters.Add(genreIdParam);
            command.Parameters.Add(movieIdParam);

            foreach(var genre in movie.Genres)
            {
                genreIdParam.Value = genre.ID;
                command.Prepare();
                command.ExecuteNonQuery();
            }

            command.Parameters.Remove(genreIdParam);
            command.CommandText = "insert_star_into_movie";

            MySqlParameter starIdParam = new("@starId", MySqlDbType.Int32);
            command.Parameters.Add(starIdParam);

            foreach(var star in movie.Stars)
            {
                starIdParam.Value = star.ID;
                command.Prepare();
                command.ExecuteNonQuery();
            }

            command.Parameters.Remove(starIdParam);
            command.CommandText = "insert_producer_into_movie";

            MySqlParameter producerIdParam = new("@producerId", MySqlDbType.Int32);
            command.Parameters.Add(producerIdParam);

            foreach(var producer in movie.Producers)
            {
                producerIdParam.Value = producer.ID;
                command.Prepare();
                command.ExecuteNonQuery();
            }

            connection.Close();


            return movie;
        }
        #endregion
    }
}
