using g2v.core.clinetsync.api.Controllers;
using System.Data.SqlClient;

namespace g2v.core.clinetsync.api
{
    public static class Comman
    {
        public static void GetHangfireConnectionString(Serilog.Core.Logger logger, string conStr)
        {
            try
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

                logger.Information("database connected");

            }
            catch (Exception ex)
            {
                logger.Error("database connection failed",ex);
            }

        }       
    }
}
