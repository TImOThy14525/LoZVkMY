// 代码生成时间: 2025-08-10 23:30:42
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CsvBatchProcessorApp
{
    /// <summary>
    /// Represents a service for processing CSV files in batch.
    /// </summary>
    public class CsvBatchProcessor
    {
        private readonly IJSRuntime _jsRuntime;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvBatchProcessor"/> class.
        /// </summary>
        /// <param name="jsRuntime">The JSRuntime instance for JavaScript interoperability.</param>
        public CsvBatchProcessor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Processes a batch of CSV files.
        /// </summary>
        /// <param name="files">The list of CSV files to process.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ProcessBatchAsync(IEnumerable<IBrowserFile> files)
        {
            if (files == null || !files.Any())
            {
                throw new ArgumentException("No files provided for processing.", nameof(files));
            }

            foreach (var file in files)
            {
                await ProcessFileAsync(file);
            }
        }

        /// <summary>
        /// Processes a single CSV file.
        /// </summary>
        /// <param name="file">The CSV file to process.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task ProcessFileAsync(IBrowserFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            try
            {
                using var stream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(stream);
                stream.Position = 0;

                using var reader = new StreamReader(stream);
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

                // Process the CSV file using CsvReader
                while (csvReader.Read())
                {
                    // Example: Print the first column value for demonstration purposes
                    string firstColumn = csvReader.GetField(0);
                    Console.WriteLine(firstColumn);
                }
            }
            catch (Exception ex)
            {
                // Log and handle exceptions
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }
    }
}
