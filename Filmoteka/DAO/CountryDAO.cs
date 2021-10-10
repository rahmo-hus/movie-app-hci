using Filmoteka.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Repository
{
    class CountryDAO
    {
        public static List<Country> GetGountries()
        {
            const string sqlCommand = "select * from mydb.countries";
            List<Country> dataList = new();
            DataTable table = DBUtil.ExecuteExtraction(sqlCommand);

            foreach (DataRow row in table.Rows)
                dataList.Add(new(row.Field<int>("CountryId"), row.Field<string>("Name")));

            return dataList;
        }

        public static Country GetCountryById(int id)
        {
            string sqlSelectCountryById = "select * from country where CountryId = " + id + ";";
            DataTable dataTable = DBUtil.ExecuteExtraction(sqlSelectCountryById);
            foreach(DataRow row in dataTable.Rows)
                return new(row.Field<int>("CountryId"), row.Field<string>("Name"));

            return new();
        }
    }
}
