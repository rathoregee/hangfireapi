using Autofac;
using Autofac.Extensions.DependencyInjection;
using g2v.core.clinetsync.api;
using Hangfire;
using Serilog;
using AutofacSerilogIntegration;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables()
                      .AddCommandLine(args)
                      .Build();
var logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(configuration)
                         .WriteTo.Console()
                         .Enrich.WithCorrelationId()
                         .CreateLogger();

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterLogger(logger);
    containerBuilder.RegisterInstance(configuration).As<IConfiguration>();
    containerBuilder.RegisterModule<AutofacModule>();    
});


//Hangfire
string conStr = @"Data Source=DESKTOP-TMRH8RM;Initial Catalog=g2vjobs;Integrated Security=true;Pooling=False";
Comman.GetHangfireConnectionString(logger, conStr);
builder.Services.AddHangfire(x => x.UseSqlServerStorage(conStr));
builder.Services.AddHangfireServer();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map to the `/g2vjobs` URL
app.UseHangfireDashboard("/g2vjobs");

BackgroundJob.Enqueue(() => Console.WriteLine("You have done your payment suceessfully!"));
RecurringJob.AddOrUpdate(
    "myrecurringjob",
    () => Console.WriteLine("Recurring! :: "+ DateTime.UtcNow.Ticks.ToString()),
    Cron.Minutely);

app.Run();