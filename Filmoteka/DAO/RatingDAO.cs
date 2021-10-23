using Filmoteka.Model;
using Filmoteka.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.DAO
{
    class RatingDAO
    {

        #region Get Ratings By Movie ID
        public static List<Rating> GetRatingsByMovieId(int movieId)
        {
            FormattableString sqlGetRatingsByMovieId = $"select * from rating where ContentId = {movieId}";
            DataTable dataTable = DBUtil.ExecuteExtraction(sqlGetRatingsByMovieId.ToString());
            List<Rating> ratings = new();
            foreach(DataRow row in dataTable.Rows)
            {
                Rating rating = new();
                rating.UserId = row.Field<int>("UserId");
                rating.RatingScore = row.Field<int>("Rating");
                rating.Timestamp = row.Field<DateTime>("Date");
                ratings.Add(rating);
            }
            return ratings;
        }
        #endregion

        #region Get ratings by user id and movie id
        public static Rating GetRatingByUserIdAndMovieId(int userId, int movieId)
        {
            FormattableString sqlGetRating = $"select * from rating where UserId={userId} and ContentId={movieId}";
            DataTable dataTable = DBUtil.ExecuteExtraction(sqlGetRating.ToString());
            Rating rating = null;
            foreach(DataRow row in dataTable.Rows)
            {
                rating = new();
                rating.UserId = userId;
                rating.RatingScore = row.Field<int>("Rating");
                rating.Timestamp = row.Field<DateTime>("Date");
            }

            return rating;
        }
        #endregion
    }
}
