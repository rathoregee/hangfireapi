using g2v.core.clinetsync.api.Controllers;
using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;
using Serilog;
using Serilog.Core;
using System.Data.SqlClient;

namespace g2v.core.clinetsync.api
{
    public class SeilogLoggerFactory
    {
        private static SeilogLoggerFactory? instance = null;

        private SeilogLoggerFactory()
        {
        }

        public static SeilogLoggerFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SeilogLoggerFactory();
                }

                return instance;
            }
        }

        public static Logger Create(IConfigurationRoot configuration)
        {
            return new LoggerConfiguration()
                         .ReadFrom.Configuration(configuration)
                         .WriteTo.Console()
                         .Enrich.WithCorrelationId()
                         .CreateLogger();
        }
    }

    public static class Comman
    {
        public static IConfigurationRoot GetConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables()
                      .AddCommandLine(args)
                      .Build();
        }
        public static void CreateHangfireDatabase(IConfigurationRoot configuration, Logger logger, string conStr)
        {
            try
            {
                string dbName = configuration["G2VDBNAME"];
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
                logger.Error("database connection failed", ex);
            }

        }

        public static BasicAuthAuthorizationFilter[] GetBasicAuthentication()
        {
            return new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions 
            { 
                RequireSsl = false,
                SslRedirect = false,
                LoginCaseSensitive = true,
                Users = new [] { 
                    new BasicAuthAuthorizationUser { 
                        Login = "123",
                        PasswordClear = "000"
                    }
                }
            })
            };
        }
    }
}
