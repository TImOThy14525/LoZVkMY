// 代码生成时间: 2025-08-08 11:21:48
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CsvBatchProcessorApp
{
    /// <summary>
    /// A Blazor component that processes multiple CSV files.
    /// </summary>
    public class CsvBatchProcessor
    {
        private const string CsvFileExtension = ".csv";
        private const string ErrorLogFile = "error_log.txt";

        /// <summary>
        /// Processes the uploaded CSV files.
        /// </summary>
        /// <param name="csvFiles">The list of uploaded CSV files.</param>
        /// <returns>A string message indicating success or failure.</returns>
        public async Task<string> ProcessCsvFiles(InputFileListEntry[] csvFiles)
        {
            if (csvFiles == null || csvFiles.Length == 0)
            {
                return "No CSV files were uploaded.";
            }

            StringBuilder successMessage = new StringBuilder();
            StringBuilder errorMessage = new StringBuilder();
            int fileCount = 0;

            foreach (var file in csvFiles)
            {
                try
                {
                    if (IsCsvFile(file.Name))
                    {
                        fileCount++;
                        using var reader = file.OpenReadStream();
                        // Process the CSV file here, for example, extract data and perform operations
                        // This is just a placeholder for actual processing logic.
                        string content = await new StreamReader(reader).ReadToEndAsync();
                        // Process the content here...

                        // Append a message indicating success for the current file
                        successMessage.AppendLine($"File {file.Name} processed successfully.");
                    }
                    else
                    {
                        errorMessage.AppendLine($"File {file.Name} is not a CSV file and will be skipped.");
                    }
                }
                catch (Exception ex)
                {
                    // Write error details to the log file
                    File.AppendAllText(ErrorLogFile, $"Error processing {file.Name}: {ex.Message}
");
                    errorMessage.AppendLine($"An error occurred while processing {file.Name}: {ex.Message}");
                }
            }

            string finalMessage = BuildFinalMessage(fileCount, successMessage, errorMessage);
            return finalMessage;
        }

        /// <summary>
        /// Checks if the file has a CSV extension.
        /// </summary>
        /// <param name="fileName">The name of the file to check.</param>
        /// <returns>True if the file is a CSV; otherwise, false.</returns>
        private bool IsCsvFile(string fileName)
        {
            return fileName.EndsWith(CsvFileExtension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Builds the final message to be returned after processing all files.
        /// </summary>
        /// <param name="fileCount">The number of CSV files processed.</param>
        /// <param name="successMessage">The success message to append.</param>
        /// <param name="errorMessage">The error message to append.</param>
        /// <returns>The final message string.</returns>
        private string BuildFinalMessage(int fileCount, StringBuilder successMessage, StringBuilder errorMessage)
        {
            StringBuilder finalMessage = new StringBuilder();

            if (fileCount > 0)
            {
                finalMessage.Append($"Processed {fileCount} CSV file(s) successfully.
");
                finalMessage.Append(successMessage.ToString());
            }

            if (errorMessage.Length > 0)
            {
                finalMessage.Append("There were errors processing some files: 
");
                finalMessage.Append(errorMessage.ToString());
            }

            return finalMessage.ToString();
        }
    }
}
