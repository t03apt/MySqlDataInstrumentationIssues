using DotNet.Testcontainers.Configurations;
using MyApp;
using MySql.Data.MySqlClient;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Testcontainers.MySql;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddOpenTelemetry()
    .ConfigureResource(ApplyResourceConfiguration)
    .WithTracing(configure =>
    {
        configure
            .ConfigureResource(ApplyResourceConfiguration);

        configure.AddMySqlDataInstrumentation(options =>
        {
            options.SetDbStatement = true;
            options.EnableConnectionLevelAttributes = true;
            options.RecordException = true;
        });

        configure.AddConsoleExporter();
    });

builder.Services.AddHostedService<Worker>();
var connectionStringProvider = new ConnectionStringProvider();
builder.Services.AddSingleton(connectionStringProvider);

var host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
TestcontainersSettings.Logger = logger;

logger.LogDebug("Starting MySql container");
var container = new MySqlBuilder().Build();
await container.StartAsync();
connectionStringProvider.ConnectionString = container.GetConnectionString();
logger.LogDebug("Started MySql container");

host.Run();

static void ApplyResourceConfiguration(ResourceBuilder resource) => resource.AddService("my-app");