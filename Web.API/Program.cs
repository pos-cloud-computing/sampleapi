using Domain.Health.Repository;
using Domain.Sample.Repository;
using Infra.Repository.Health;
using Infra.Repository.Sample;
using Microsoft.OpenApi.Models;
using Prometheus;
using Prometheus.DotNetRuntime;

var builder = WebApplication.CreateBuilder(args);
string applicationName = "skyactivationapi";

var applicationNameEnvironment = Environment.GetEnvironmentVariable("APPLICATION_NAME");

if (!string.IsNullOrEmpty(applicationNameEnvironment))
	applicationName = applicationNameEnvironment;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = applicationName, Version = "v1" });
});

builder.Services.AddApiVersioning();

//Dependency Injection   
builder.Services.AddScoped<IHealthRepository, HealthRepository>();
builder.Services.AddScoped<IExampleRepository, ExampleRepository>();

builder.Services.AddHttpLogging(httpLogging =>
{
	httpLogging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

var app = builder.Build();
app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpMetrics();

Metrics.DefaultRegistry.SetStaticLabels(new Dictionary<string, string>
			{
				{ "application", applicationName }
			});

app.UseMetricServer();

app.UseHttpsRedirection();

app.UseAuthorization();



IDisposable collector = DotNetRuntimeStatsBuilder
							.Customize()
							.WithContentionStats(CaptureLevel.Informational)
							.WithGcStats(CaptureLevel.Verbose)
							.WithThreadPoolStats(CaptureLevel.Informational)
							.WithExceptionStats(CaptureLevel.Errors)
							.WithJitStats()
							.RecycleCollectorsEvery(new TimeSpan(0, 20, 0)).StartCollecting();

app.MapControllers();

app.Run();

