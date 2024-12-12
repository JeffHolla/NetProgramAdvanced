using System.Data;
using System.Data.Common;
using CatalogService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CatalogService.Application.FunctionalTests;

public class PostgresTestDatabase : ITestDatabase
{
    private readonly string _connectionString;
    private readonly NpgsqlConnection _connection;

    public PostgresTestDatabase()
    {
        _connectionString = "DataSource=:memory:";
        _connection = new NpgsqlConnection(_connectionString);
    }

    public async Task InitialiseAsync()
    {
        if (_connection.State == ConnectionState.Open)
        {
            await _connection.CloseAsync();
        }

        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connection)
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.Migrate();
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await InitialiseAsync();
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
