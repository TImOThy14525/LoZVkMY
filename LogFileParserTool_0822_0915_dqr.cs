// 代码生成时间: 2025-08-22 09:15:16
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace LogFileParserTool
{
    /// <summary>
    /// LogFileParser is a component responsible for parsing log files.
    /// </summary>
    public partial class LogFileParser 
    {
        [Parameter]
        public string LogFilePath { get; set; }

        private List<string> logEntries = new List<string>();
        private bool hasError = false;
        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            await ParseLogFileAsync();
        }

        /// <summary>
        /// Parse log file asynchronously.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task ParseLogFileAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(LogFilePath))
                {
                    hasError = true;
                    errorMessage = "Log file path is not provided.";
                    StateHasChanged();
                    return;
                }

                if (!File.Exists(LogFilePath))
                {
                    hasError = true;
                    errorMessage = "Log file does not exist.";
                    StateHasChanged();
                    return;
                }

                logEntries = await File.ReadAllLinesAsync(LogFilePath);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = ex.Message;
                StateHasChanged();
            }
        }

        /// <summary>
        /// Render log entries or error message.
        /// </summary>
        /// <returns>RenderFragment representing the UI components.</returns>
        private RenderFragment RenderLogEntriesOrError()
        {
            if (hasError)
            {
                return builder => builder.AddContent(0, $"<p>Error: {errorMessage}</p>");
            }
            else
            {
                return builder => builder.AddContent(0, logEntries);
            }
        }

        /// <summary>
        /// Render the log parser component UI.
        /// </summary>
        /// <returns>RenderFragment representing the UI components.</returns>
        private RenderFragment RenderLogParserUI()
        {
            return builder =>
            {
                builder.OpenComponent<LogParserComponent>(0);
                builder.AddAttribute(1, "LogFilePath", LogFilePath);
                builder.CloseComponent();
            };
        }
    }

    /// <summary>
    /// LogParserComponent is a child component that displays log entries.
    /// </summary>
    public class LogParserComponent : ComponentBase
    {
        [Parameter]
        public string LogFilePath { get; set; }

        private List<string> logEntries = new List<string>();

        protected override void OnInitialized()
        {
            if (!string.IsNullOrEmpty(LogFilePath))
            {
                logEntries = File.ReadAllLines(LogFilePath).ToList();
            }
        }

        public RenderFragment RenderLogEntries()
        {
            return builder => builder.AddContent(0, logEntries);
        }
    }
}
