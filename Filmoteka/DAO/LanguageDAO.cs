using Filmoteka.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Repository
{
    class LanguageDAO
    {
        public static List<Language> GetLanguages()
        {
            const string sqlCommand = "select * from languages";
            List<Language> dataList = new();
            DataTable table = DBUtil.ExecuteExtraction(sqlCommand);

            foreach (DataRow row in table.Rows)
                dataList.Add(new(row.Field<int>("LanguageId"), row.Field<string>("Name")));

            return dataList;
        }

        public static Language GetLanguageById(int id)
        {
            string sqlSelectLanguageById = "select * from language where LanguageId = " + id + ";";
            DataTable dataTable = DBUtil.ExecuteExtraction(sqlSelectLanguageById);
            foreach (DataRow row in dataTable.Rows)
                return new(row.Field<int>("LanguageId"), row.Field<string>("Name"));
            return new();
        }
    }
}
