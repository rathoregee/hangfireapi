using System.Data.SqlClient;

namespace g2v.core.clinetsync.api
{
    public static class Comman
    {
        public static void GetHangfireConnectionString(string conStr)
        {
            string dbName = "g2vjobs";
            conStr = conStr.Replace(dbName, "master");

            using var con = new SqlConnection(conStr);
            con.Open();

            using var command = new SqlCommand(string.Format(
                @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') 
                                    create database [{0}];
                      ", dbName), con);
            command.ExecuteNonQuery();

        }       
    }
}
