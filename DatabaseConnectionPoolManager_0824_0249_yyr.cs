// 代码生成时间: 2025-08-24 02:49:58
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

// DatabaseConnectionPoolManager class to manage a pool of SQL Server connections.
public class DatabaseConnectionPoolManager
{
    private readonly ConcurrentBag<SqlConnection> _availableConnections;
    private readonly string _connectionString;
    private readonly int _maxPoolSize;
    private readonly int _minPoolSize;
    private int _currentPoolSize;

    // Constructor to initialize the connection pool with the minimum and maximum pool sizes.
    public DatabaseConnectionPoolManager(string connectionString, int minPoolSize, int maxPoolSize)
    {
        if (minPoolSize > maxPoolSize)
        {
            throw new ArgumentException("Minimum pool size cannot be greater than the maximum pool size.");
        }

        _connectionString = connectionString;
        _minPoolSize = minPoolSize;
        _maxPoolSize = maxPoolSize;
        _availableConnections = new ConcurrentBag<SqlConnection>();
        _currentPoolSize = 0;

        // Initialize the pool with the minimum number of connections.
        InitializePool();
    }

    // Initialize the pool with the minimum number of connections.
    private void InitializePool()
    {
        for (int i = 0; i < _minPoolSize; i++)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            _availableConnections.Add(connection);
            _currentPoolSize++;
        }
    }

    // Get a connection from the pool.
    public async Task<SqlConnection> GetConnection()
    {
        if (_availableConnections.TryTake(out SqlConnection? connection))
        {
            return connection;
        }
        else
        {
            // If pool is empty and current size is less than max, create a new connection.
            if (_currentPoolSize < _maxPoolSize)
            {
                var newConnection = new SqlConnection(_connectionString);
                await newConnection.OpenAsync();
                _currentPoolSize++;
                return newConnection;
            }
            else
            {
                throw new InvalidOperationException("Connection pool is exhausted.");
            }
        }
    }

    // Return a connection back to the pool.
    public void ReturnConnection(SqlConnection connection)
    {
        if (connection.State == System.Data.ConnectionState.Open)
        {
            _availableConnections.Add(connection);
        }
        else
        {
            // If the connection is closed, remove it from the pool and decrement the size.
            connection.Dispose();
            _currentPoolSize--;
        }
    }

    // Dispose the connection pool and free the resources.
    public void Dispose()
    {
        foreach (var connection in _availableConnections)
        {
            connection.Dispose();
        }
        _availableConnections.Clear();
    }
}
