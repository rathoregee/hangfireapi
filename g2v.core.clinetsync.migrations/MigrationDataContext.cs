using g2v.core.clinetsync.dataaccess.Classes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace g2v.core.clinetsync.migrations.Migrations
{
    public class MigrationDataContext : DataContext
    {
        private readonly IConfigurationRoot _configuration;

        public MigrationDataContext()
        {
            _configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var database = _configuration["DATABASE"];
            //var host = _configuration["PGHOST"];
            //var password = _configuration["PGPASSWORD"];
            //var port = _configuration["PGPORT"];
            //var user = _configuration["PGUSER"];

            //var connectionString = $"Server={host};port={port};user id={user};password={password};database={database};pooling=true";
            var connectionString = $"Data Source=DESKTOP-TMRH8RM;Initial Catalog=g2vjobs;Integrated Security=true;Pooling=False";


            //optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
