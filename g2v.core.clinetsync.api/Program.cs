using Autofac;
using Autofac.Extensions.DependencyInjection;
using g2v.core.clinetsync.api;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
//    .ConfigureContainer<ContainerBuilder>(builder =>
//    {
//        //register with autofac 
//        builder.RegisterModule(new AutofacModule());
//    });

//Hangfire
string conStr = @"Data Source=DESKTOP-TMRH8RM;Initial Catalog=g2vjobs;Integrated Security=true;Pooling=False";
Comman.GetHangfireConnectionString(conStr);
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
    () => Console.WriteLine("Recurring!"),
    Cron.Minutely);

app.Run();