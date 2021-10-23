using Filmoteka.Model;
using Filmoteka.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.DAO
{
    class StarDAO
    {

        #region Update
        public static bool Update(Star star)
        {
            FormattableString sqlUpdateStar = $"update person set FirstName='{star.FirstName}', LastName='{star.LastName}', Gender='{star.Gender}', DateOfBirth='{star.DateOfBirth}', Bio='{star.Bio}' where PersonId = {star.ID}";
            int success = DBUtil.ExecuteCommand(sqlUpdateStar.ToString());
            if (success != 0 && star.Image != null)
            {
                using MySqlConnection mySqlConnection = new(ConfigurationManager.AppSettings["connectionString"]);
                MySqlCommand imageInsertCommand = new("update person set Image=@image where PersonId=" + star.ID, mySqlConnection);
                mySqlConnection.Open();
                MySqlParameter imageParam = new("@image", MySqlDbType.LongBlob);
                imageParam.Value = ImageUtil.ConvertBitmapToByteArr(star.Image);
                imageInsertCommand.Parameters.Add(imageParam);
                imageInsertCommand.Prepare();
                imageInsertCommand.ExecuteNonQuery();
                mySqlConnection.Close();

                return true;
            }
            else return false;
           
        }
        #endregion

        #region Delete
        public static bool Delete(int id)
        {
            FormattableString sqlDeleteStarData = $"delete from starmediacontent where StarId = {id}; delete from star where StarId = {id}; delete from person where PersonId = {id};";
            int success = DBUtil.ExecuteCommand(sqlDeleteStarData.ToString());
            return success != 0;
        }
        #endregion

        #region Save 
        public static Star Save(Star star)
        {
            FormattableString sqlInsertStarData = $"insert into person (FirstName, LastName, Gender, DateOfBirth, Bio) values ('{star.FirstName}','{star.LastName}', '{star.Gender}', '{star.DateOfBirth}', '{star.Bio}')";
            string sqlSelectLastInserted = "select last_insert_id()";
            int success = DBUtil.ExecuteCommand(sqlInsertStarData.ToString());
            if (success != 0)
            {
                DataTable dataTable = DBUtil.ExecuteExtraction(sqlSelectLastInserted);
                foreach (DataRow row in dataTable.Rows)
                    star.ID = Convert.ToInt32(row.Field<UInt64>("last_insert_id()"));
                DBUtil.ExecuteCommand("insert into star (StarId) values (" + star.ID + ")");

                using MySqlConnection mySqlConnection = new(ConfigurationManager.AppSettings["connectionString"]);
                MySqlCommand imageInsertCommand = new("update person set Image=@image where PersonId=" + star.ID, mySqlConnection);
                mySqlConnection.Open();
                MySqlParameter imageParam = new("@image", MySqlDbType.LongBlob);
                imageParam.Value = ImageUtil.ConvertBitmapToByteArr(star.Image);
                imageInsertCommand.Parameters.Add(imageParam);
                imageInsertCommand.Prepare();
                imageInsertCommand.ExecuteNonQuery();
                mySqlConnection.Close();

            }
            else return null;
            
            return star;
        }
        #endregion 

        #region Get Cast By movie Id
        public static List<Star> GetCastByMovieId(int movieId)
        {
            string sqlSelectCastByMovieId = "select s.StarId, p.FirstName, p.LastName, p.Gender, p.DateOfBirth,p.Image, p.Bio from person p" +
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
                    row.Field<string>("Bio"),
                    ImageUtil.ConvertByteArrToBitmap(row.Field<byte[]>("Image"))
                ));

            return stars;
        }
        #endregion

        #region Get all Cast
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
                        row.Field<string>("Bio"),
                        ImageUtil.ConvertByteArrToBitmap(row.Field<byte[]>("Image"))
                    ));

            return dataList;
        }
    }
    #endregion
}
