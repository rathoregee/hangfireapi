using Autofac;
using Autofac.Extensions.DependencyInjection;
using g2v.core.clinetsync.api;
using Hangfire;
using AutofacSerilogIntegration;
using Hangfire.Dashboard;
using Hangfire.AspNetCore;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = Comman.GetConfiguration(args);

var logger = SeilogLoggerFactory.Create(configuration);

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterLogger(logger);
    containerBuilder.RegisterInstance(configuration).As<IConfiguration>();
    containerBuilder.RegisterModule<AutofacModule>();
});


//Hangfire
string conStr = configuration.GetConnectionString("ConnectionString");

Comman.CreateHangfireDatabase(configuration, logger, conStr);
builder.Services.AddHangfire(x => x.UseSqlServerStorage(conStr));
builder.Services.AddHangfireServer();
// Signal R Core
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader(); 
                      });
});

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
app.UseHangfireDashboard(configuration["HANGFIREURL"], new DashboardOptions
{
    Authorization = Comman.GetBasicAuthentication()
});

BackgroundJob.Enqueue(() => Console.WriteLine("You have done your payment suceessfully!"));
RecurringJob.AddOrUpdate(
    "myrecurringjob",
    () => Console.WriteLine("Recurring! :: " + DateTime.UtcNow.Ticks.ToString()),
    Cron.Minutely);

app.MapHub<ChatHub>("/chatHub");

app.Run();