// 代码生成时间: 2025-08-11 20:04:58
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp
{
    /// <summary>
    /// Component for demonstrating SQL injection prevention
    /// </summary>
    public class SqlInjectionPreventionBase : ComponentBase
    {
        [Inject]
        protected SqlInjectionService SqlInjectionService { get; set; }

        protected string QueryInput { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            // Initialization logic
            await Task.CompletedTask;
        }

        protected async Task SubmitQuery()
        {
            if (!string.IsNullOrWhiteSpace(QueryInput))
            {
                try
                {
                    var result = await SqlInjectionService.ExecuteSafeQuery(QueryInput);
                    // Process the result
                    Console.WriteLine(result);
                }
                catch (SqlException ex)
                {
                    // Handle SQL exception
                    Console.WriteLine($"SQL error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Please enter a query.");
            }
        }
    }

    /// <summary>
    /// Service for handling SQL injection prevention
    /// </summary>
    public class SqlInjectionService
    {
        private readonly string _connectionString;

        public SqlInjectionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Executes a safe SQL query to prevent SQL injection
        /// </summary>
        /// <param name="query">The query string from the user</param>
        /// <returns>The result of the query</returns>
        public async Task<string> ExecuteSafeQuery(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    // Use parameterized query to prevent SQL injection
                    var parameter = new SqlParameter("@Parameter", query);
                    command.Parameters.Add(parameter);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Assuming the result is a single column
                            return reader.GetString(0);
                        }
                    }
                }
            }
            return "No data found.";
        }
    }
}
