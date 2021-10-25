using Filmoteka.Model;
using Filmoteka.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Filmoteka.DAO
{
    class MovieDAO
    {
        public static readonly string connectionString = ConfigurationManager.AppSettings["connectionString"];

        #region Get Movie By ID
        public static Movie GetById(int movieId)
        {
            FormattableString sqlGetMovieById = $"select mov.MovieId, med.Name, med.Description, med.Budget, med.CountryId, med.LanguageId,med.Image,  mov.Duration from mediacontent med inner join movie mov on mov.MovieId = med.ContentId where mov.MovieId={movieId}";

            DataTable dataTable = DBUtil.ExecuteExtraction(sqlGetMovieById.ToString());
            Movie movie = null;
            foreach(DataRow row in dataTable.Rows)
            {
                movie = new();
                movie.ID = row.Field<int>("MovieId");
                movie.Name = row.Field<string>("Name");
                movie.Description = row.Field<string>("Description");
                movie.Duration = row.Field<int>("Duration");
                movie.Image = ImageUtil.ConvertByteArrToBitmap(row.Field<byte[]>("Image"));
                movie.OriginCountry = CountryDAO.GetCountryById(row.Field<int>("CountryId"));
                movie.Language = LanguageDAO.GetLanguageById(row.Field<int>("LanguageId"));
                movie.Producers = ProducerDAO.GetProducersByMovieId(movie.ID);
                movie.Stars = StarDAO.GetCastByMovieId(movie.ID);
                movie.Genres = GenreDAO.GetGenresByMovieId(movie.ID);
                movie.Ratings = RatingDAO.GetRatingsByMovieId(movie.ID);
            }
            return movie;
        }
        #endregion

        #region Get All Movies
        public static List<Movie> GetAllMovies()
        {
            const string sqlMovieDataStatement = "select mov.MovieId, med.Name, med.Description, med.Budget, med.CountryId, med.LanguageId,med.Image,  mov.Duration from mediacontent med " +
                "inner join movie mov on mov.MovieId = med.ContentId;";

            DataTable movieContentDataTable = DBUtil.ExecuteExtraction(sqlMovieDataStatement);

            List<Movie> movies = new();

            foreach(DataRow row in movieContentDataTable.Rows)
            {
                Movie localMovie = new();
                localMovie.ID = row.Field<int>("MovieId");
                localMovie.Name = row.Field<string>("Name");
                localMovie.Description = row.Field<string>("Description");
                localMovie.Duration = row.Field<int>("Duration");
                localMovie.Image = ImageUtil.ConvertByteArrToBitmap(row.Field<byte[]>("Image"));
                localMovie.OriginCountry = CountryDAO.GetCountryById(row.Field<int>("CountryId"));
                localMovie.Language = LanguageDAO.GetLanguageById(row.Field<int>("LanguageId"));
                localMovie.Budget = row.Field<float>("Budget");
                localMovie.Producers = ProducerDAO.GetProducersByMovieId(localMovie.ID);
                localMovie.Stars = StarDAO.GetCastByMovieId(localMovie.ID);
                localMovie.Genres = GenreDAO.GetGenresByMovieId(localMovie.ID);
                localMovie.Ratings = RatingDAO.GetRatingsByMovieId(localMovie.ID);
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
            string sqlUpdateMovieData = "update mediacontent med inner join movie mov on mov.MovieId = med.ContentId set " +
                "med.Name = \""+movie.Name+"\", med.Description =\""+movie.Description+"\", med.CountryId = "+movie.OriginCountry.ID+", " +
                "med.LanguageId = "+movie.Language.ID+", mov.Duration = "+movie.Duration+", med.Budget = "+movie.Budget +
                " where mov.MovieId ="+movie.ID+";";
            //", med.Image = "+ConvertBitmapToByteArr(movie.Image)+"

            string sqlDeleteStatements = "delete from producermediacontent pmc where pmc.ContentId =" + movie.ID + ";" +
                "delete from starmediacontent smc where smc.ContentId =" + movie.ID + ";" +
                "delete from genremediacontent gmc where gmc.ContentId =" + movie.ID + ";";

            string updateProducers = "insert into producermediacontent values("+movie.ID+",?); ";
            string updateStars = "insert into starmediacontent values(" + movie.ID + ",?); ";
            string updateGenres ="insert into genremediacontent(ContentId, GenreId) values("+movie.ID+",?); ";


            DBUtil.ExecuteCommand(sqlUpdateMovieData);
            DBUtil.ExecuteCommand(sqlDeleteStatements);
            movie.Producers.ForEach(producer =>  DBUtil.ExecuteCommand(updateProducers.Replace("?", producer.ID.ToString())));
            movie.Stars.ForEach(star => DBUtil.ExecuteCommand(updateStars.Replace("?", star.ID.ToString())));
            movie.Genres.ForEach(genre => DBUtil.ExecuteCommand(updateGenres.Replace("?", genre.ID.ToString())));
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
            MySqlParameter imageParam = new("@image", MySqlDbType.LongBlob);

            nameParam.Value = movie.Name;
            descriptionParam.Value = movie.Description;
            budgetParam.Value = movie.Budget;
            countryParam.Value = movie.OriginCountry.ID;
            languageParam.Value = movie.Language.ID;
            durationParam.Value = movie.Duration;
            imageParam.Value = movie.Image!=null ? ImageUtil.ConvertBitmapToByteArr(movie.Image) : null;

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

        #region Rate movie
        public static bool RateMovie(int movieId, int rating, int userId)
        {
            FormattableString sqlRateMovie = $"insert into rating (ContentId, UserId, Rating, Date) values ({movieId}, {userId}, {rating}, CURRENT_TIMESTAMP) on duplicate key update Date=values(Date), Rating=values(Rating); ";

            DBUtil.ExecuteCommand(sqlRateMovie.ToString());
            return true;
        }
        #endregion
    }
}
