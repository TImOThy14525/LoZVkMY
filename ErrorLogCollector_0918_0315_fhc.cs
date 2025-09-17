// 代码生成时间: 2025-09-18 03:15:20
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp
{
    /// <summary>
    /// A component for collecting and storing error logs.
    /// </summary>
    public partial class ErrorLogCollector
    {
        [Inject] private NavigationManager NavigationManager { get; set; }

        private List<ErrorLog> errorLogs = new List<ErrorLog>();
        private ErrorLog errorLog = new ErrorLog();

        /// <summary>
        /// Invoked when the component is initialized.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            LoadErrorLogs();
        }

        /// <summary>
        /// Invoked when the error is captured.
        /// </summary>
        public async Task LogErrorAsync(string message, Exception exception)
        {
            errorLog.Message = message;
            errorLog.Exception = exception?.ToString();
            errorLog.Timestamp = DateTime.Now;

            errorLogs.Add(errorLog);
            await SaveErrorLogsAsync();
        }

        /// <summary>
        /// Loads the error logs from a file.
        /// </summary>
        private void LoadErrorLogs()
        {
            try
            {
                string errorLogFilePath = GetErrorLogFilePath();

                if (File.Exists(errorLogFilePath))
                {
                    string logsJson = File.ReadAllText(errorLogFilePath);
                    errorLogs = JsonSerializer.Deserialize<List<ErrorLog>>(logsJson);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading error logs: " + ex.Message);
            }
        }

        /// <summary>
        /// Saves the error logs to a file.
        /// </summary>
        private async Task SaveErrorLogsAsync()
        {
            try
            {
                string errorLogFilePath = GetErrorLogFilePath();

                string logsJson = JsonSerializer.Serialize(errorLogs, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(errorLogFilePath, logsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving error logs: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the file path for storing error logs.
        /// </summary>
        private string GetErrorLogFilePath()
        {
            return "error-logs.json";
        }

        /// <summary>
        /// Represents an error log entry.
        /// </summary>
        private class ErrorLog
        {
            public string Message { get; set; }
            public string Exception { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}
