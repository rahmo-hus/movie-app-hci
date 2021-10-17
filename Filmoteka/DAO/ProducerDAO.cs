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

namespace Filmoteka.Repository
{
    class ProducerDAO
    {
        #region Save 
        public static Producer Save(Producer producer)
        {
            FormattableString sqlInsertProducerData = $"insert into person (FirstName, LastName, Gender, DateOfBirth, Bio) values ('{producer.FirstName}','{producer.LastName}', '{producer.Gender}', '{producer.DateOfBirth}', '{producer.Bio}')";
            string sqlSelectLastInserted = "select last_insert_id()";
            int success = DBUtil.ExecuteCommand(sqlInsertProducerData.ToString());
            if (success != 0)
            {
                DataTable dataTable = DBUtil.ExecuteExtraction(sqlSelectLastInserted);
                foreach (DataRow row in dataTable.Rows)
                    producer.ID = Convert.ToInt32(row.Field<UInt64>("last_insert_id()"));
                DBUtil.ExecuteCommand("insert into producer values (" + producer.ID + ")");

                using MySqlConnection mySqlConnection = new(ConfigurationManager.AppSettings["connectionString"]);
                MySqlCommand imageInsertCommand = new("update person set Image=@image where PersonId=" + producer.ID, mySqlConnection);
                mySqlConnection.Open();
                MySqlParameter imageParam = new("@image", MySqlDbType.LongBlob);
                imageParam.Value = ImageUtil.ConvertBitmapToByteArr(producer.Image);
                imageInsertCommand.Parameters.Add(imageParam);
                imageInsertCommand.Prepare();
                imageInsertCommand.ExecuteNonQuery();
                mySqlConnection.Close();

            }
            else return null;

            return producer;
        }
        #endregion

        #region Update
        public static bool Update(Producer producer)
        {
            FormattableString sqlUpdateProducer = $"update person set FirstName='{producer.FirstName}', LastName='{producer.LastName}', Gender='{producer.Gender}', DateOfBirth='{producer.DateOfBirth}', Bio='{producer.Bio}' where PersonId = {producer.ID}";
            int success = DBUtil.ExecuteCommand(sqlUpdateProducer.ToString());
            if (success != 0 && producer.Image != null)
            {
                using MySqlConnection mySqlConnection = new(ConfigurationManager.AppSettings["connectionString"]);
                MySqlCommand imageInsertCommand = new("update person set Image=@image where PersonId=" + producer.ID, mySqlConnection);
                mySqlConnection.Open();
                MySqlParameter imageParam = new("@image", MySqlDbType.LongBlob);
                imageParam.Value = ImageUtil.ConvertBitmapToByteArr(producer.Image);
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
            FormattableString sqlDeleteProducerData = $"delete from producermediacontent where ProducerId = {id}; delete from producer where ProducerId = {id}; delete from person where PersonId = {id};";
            int success = DBUtil.ExecuteCommand(sqlDeleteProducerData.ToString());
            return success != 0;
        }
        #endregion

        #region Get Producer By Movie ID
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
                                       row.Field<string>("Nickname"),
                                       ImageUtil.ConvertByteArrToBitmap(row.Field<byte[]>("Image"))
                                   ));
            return producers;
        }
        #endregion

        #region Get All
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
                        row.Field<string>("Nickname"),
                        ImageUtil.ConvertByteArrToBitmap(row.Field<byte[]>("Image"))
                    ));

            return dataList;
        }
        #endregion
    }
}
