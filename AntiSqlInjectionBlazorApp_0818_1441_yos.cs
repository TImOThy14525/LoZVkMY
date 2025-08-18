// 代码生成时间: 2025-08-18 14:41:00
using Microsoft.AspNetCore.Components;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlazorApp
{
    public partial class AntiSqlInjectionBlazorApp
    {
        [Inject]
        private ISqlService SqlService { get; set; }

        // Call this method to perform a query that is not vulnerable to SQL injection
        public async Task PerformSafeQueryAsync(string userInput)
        {
            try
            {
                // Use parameterized queries to prevent SQL injection
                string query = "SELECT * FROM Users WHERE Username = @Username";
                using (var connection = SqlService.GetSqlConnection())
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", userInput);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Process the data from the reader
                            // For example, display user details
                            // Console.WriteLine(reader["Username"] + ": " + reader["Email"]);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("An SQL error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}

// This is a service that provides a connection to the SQL database and handles the connection pool.
public interface ISqlService
{
    SqlConnection GetSqlConnection();
}

// Implementation of the ISqlService that sets up the connection string and returns a new connection.
public class SqlService : ISqlService
{
    private readonly string _connectionString;

    public SqlService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlConnection GetSqlConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
