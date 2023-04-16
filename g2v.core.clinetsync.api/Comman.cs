using g2v.core.clinetsync.api.Controllers;
using Hangfire.Dashboard;
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

        public static BasicAuthAuthorizationFilter AuthAuthorizationFilter()
        {
            return new BasicAuthAuthorizationFilter(
                new BasicAuthAuthorizationFilterOptions
                {
                    // Require secure connection for dashboard
                    RequireSsl = true,
                    // Case sensitive login checking
                    LoginCaseSensitive = true,
                    // Users
                    Users = new[]
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login = "g2v",
                            // Password as plain text, SHA1 will be used
                            PasswordClear = "g2v"
                        },
                        new BasicAuthAuthorizationUser
                        {
                            Login = "Administrator-2",
                            // Password as SHA1 hash
                            Password = new byte[]{0xa9,
                                0x4a, 0x8f, 0xe5, 0xcc, 0xb1, 0x9b,
                                0xa6, 0x1c, 0x4c, 0x08, 0x73, 0xd3,
                                0x91, 0xe9, 0x87, 0x98, 0x2f, 0xbb,
                                0xd3}
                        }
                    }
                });
        }
    }

    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return context.GetHttpContext().User.IsInRole(@"AD group");
        }
    }
}
