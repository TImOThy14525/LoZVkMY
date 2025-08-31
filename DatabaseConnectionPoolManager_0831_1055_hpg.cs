// 代码生成时间: 2025-08-31 10:55:48
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

// DatabaseConnectionPoolManager is responsible for managing a pool of database connections.
public class DatabaseConnectionPoolManager
{
    private readonly ConcurrentBag<DbConnection> _connectionPool;
    private readonly SemaphoreSlim _semaphore;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<DatabaseConnectionPoolManager> _logger;
    private readonly int _maxConnections;
    private readonly int _minConnections;
    private readonly TimeSpan _connectionLifeTime;

    // Initializes a new instance of the DatabaseConnectionPoolManager class.
    public DatabaseConnectionPoolManager(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<DatabaseConnectionPoolManager> logger,
        int maxConnections,
        int minConnections,
        TimeSpan connectionLifeTime)
    {
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _maxConnections = maxConnections;
        _minConnections = minConnections;
        _connectionLifeTime = connectionLifeTime;
        _connectionPool = new ConcurrentBag<DbConnection>();
        _semaphore = new SemaphoreSlim(1);
    }

    // Gets a database connection from the pool or creates a new one if none are available.
    public async Task<DbConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (_connectionPool.TryTake(out var connection))
            {
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                }
                return connection;
            }
            else
            {
                _logger.LogInformation("No available connections in the pool. Creating a new one.");
                return CreateNewConnection();
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // Creates a new database connection.
    private DbConnection CreateNewConnection()
    {
        var connection = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DbConnection>();
        if (connection.State != ConnectionState.Closed)
        {
            throw new InvalidOperationException("Connection should be closed before being added to the pool.");
        }
        _connectionPool.Add(connection);
        _logger.LogInformation("New connection created and added to the pool.");
        return connection;
    }

    // Returns a database connection to the pool, or disposes of it if it has exceeded its lifetime.
    public async Task ReleaseConnectionAsync(DbConnection connection, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (connection.State != ConnectionState.Closed)
            {
                await connection.CloseAsync();
            }
            if (DateTime.UtcNow - connection.DataSourceCreated.Add(_connectionLifeTime) > DateTime.UtcNow)
            {
                _logger.LogInformation("Connection exceeded its lifetime. Disposing it.");
                connection.Dispose();
            }
            else
            {
                _connectionPool.Add(connection);
                _logger.LogInformation("Connection returned to the pool.");
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // Closes all connections in the pool and disposes of them.
    public async Task DisposeAllConnectionsAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            foreach (var connection in _connectionPool)
            {
                connection.Dispose();
            }
            _connectionPool.Clear();
            _logger.LogInformation("All connections in the pool have been disposed.");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
