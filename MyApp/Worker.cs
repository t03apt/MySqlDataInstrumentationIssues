using MySql.Data.MySqlClient;

namespace MyApp;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ConnectionStringProvider _connectionStringProvider;

    public Worker(ILogger<Worker> logger, ConnectionStringProvider connectionStringProvider)
    {
        _logger = logger;
        _connectionStringProvider = connectionStringProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_connectionStringProvider.ConnectionString != null)
            {
                _logger.LogInformation("Sending MySql command");

                using var connection = new MySqlConnection(_connectionStringProvider.ConnectionString);
                await connection.OpenAsync(stoppingToken);
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT 1";
                await command.ExecuteScalarAsync(stoppingToken);
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}
