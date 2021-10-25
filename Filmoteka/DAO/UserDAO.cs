using Filmoteka.Model;
using System;
using System.Data;

namespace Filmoteka.DAO
{
    class UserDAO
    {
        public static User Login(string username, string password)
        {
            FormattableString sqlCommand = $"select u.UserId, u.username, u.password, r.RoleId from user u inner join userrole r on r.UserId=u.UserId where username='{username}' and password='{password}'";
            DataTable table = DBUtil.ExecuteExtraction(sqlCommand.ToString());
            User user = null;
            foreach(DataRow row in table.Rows)
            {
                user = new();
                user.ID = row.Field<int>("UserId");
                user.Username = row.Field<string>("username");
                user.Password = row.Field<string>("password");
                user.Role = row.Field<int>("RoleId") == 2 ? ERole.VIEWER : ERole.ADMIN;
                break;
            }

            return user;
        }
    }
}
