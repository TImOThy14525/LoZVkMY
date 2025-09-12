// 代码生成时间: 2025-09-12 11:13:44
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CsvBatchProcessorApp
{
    public class CsvBatchProcessor
    {
        [Inject]
        private IJSRuntime JS { get; set; }

        // Method to process a single CSV file
        public async Task ProcessSingleCsvFile(string filePath)
        {
            try
            {
                // Read all lines from the file
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    // Process each line as needed (example: parse CSV)
                    var csvLine = line.Split(',');
                    // Perform operations with the CSV line, e.g., convert to JSON
                    var jsonLine = JsonSerializer.Serialize(csvLine);

                    // Use JS interop to display the result in the browser
                    await JS.InvokeVoidAsync("displayCsvLine", jsonLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }

        // Method to process a batch of CSV files
        public async Task ProcessCsvBatch(string directoryPath)
        {
            try
            {
                // Get all CSV files from the directory
                var csvFiles = Directory.EnumerateFiles(directoryPath, "*.csv").ToList();

                foreach (var filePath in csvFiles)
                {
                    // Process each CSV file
                    await ProcessSingleCsvFile(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing batch: {ex.Message}");
            }
        }
    }
}
