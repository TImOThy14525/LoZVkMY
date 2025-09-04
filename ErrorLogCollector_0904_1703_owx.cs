// 代码生成时间: 2025-09-04 17:03:25
 * Features:
 * - Error capturing from within event handlers.
 * - Error logging to a file or a database (implementation depends on specific requirements).
 * - Basic error handling to prevent application crashes due to unhandled exceptions.
 *
 * Usage:
 * - Include this component within your Blazor application,
 *   and ensure that it is initialized appropriately.
 */

using Microsoft.AspNetCore.Components;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorApp.Logging
{
    public class ErrorLogCollector : ComponentBase
    {
        // A path to save error logs, adjust as needed
        private const string LogFilePath = "error_logs.txt";

        [Parameter]
        public EventCallback OnErrorLogged { get; set; }

        // Method to invoke when an error occurs
        public async Task LogError(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            // Format the error message
            string errorMessage = $"Error: {exception.Message}
Stack Trace: {exception.StackTrace}
";

            // Log the error to the file system
            await LogToFile(errorMessage);

            // Invoke callback if provided
            OnErrorLogged.InvokeAsync(exception);
        }

        // Writes an error message to a file, ensures thread safety
        private async Task LogToFile(string message)
        {
            await Task.Run(() =>
            {
                string directory = Path.GetDirectoryName(LogFilePath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                lock (LogFilePath)
                {
                    File.AppendAllText(LogFilePath, message + Environment.NewLine);
                }
            });
        }
    }
}