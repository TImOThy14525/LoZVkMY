// 代码生成时间: 2025-09-22 15:24:35
using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Components;

namespace FileDecompressionTool
{
    /// <summary>
    /// A Blazor component that provides file decompression functionality.
    /// </summary>
    public partial class FileDecompressionTool
    {
        [Parameter]
        public EventCallback OnDecompressionComplete { get; set; }

        private string _inputFilePath;
        private string _outputFolderPath;

        /// <summary>
        /// Input file path for the compressed file.
        /// </summary>
        public string InputFilePath
        {
            get => _inputFilePath;
            set
            {
                _inputFilePath = value;
                // Trigger the decompression process when the file path is set.
                DecompressFile();
            }
        }

        /// <summary>
        /// Output folder path where the decompressed files will be saved.
        /// </summary>
        public string OutputFolderPath
        {
            get => _outputFolderPath;
            set => _outputFolderPath = value;
        }

        /// <summary>
        /// Decompresses the file at the specified input file path to the specified output folder path.
        /// </summary>
        private async Task DecompressFile()
        {
            if (string.IsNullOrEmpty(InputFilePath))
            {
                Console.WriteLine("Input file path is not provided.");
                return;
            }

            if (!File.Exists(InputFilePath))
            {
                Console.WriteLine("Input file does not exist.");
                return;
            }

            if (string.IsNullOrEmpty(OutputFolderPath))
            {
                OutputFolderPath = Path.GetDirectoryName(InputFilePath);
            }

            try
            {
                // Ensure the output folder exists.
                Directory.CreateDirectory(OutputFolderPath);

                // Decompress the file.
                ZipFile.ExtractToDirectory(InputFilePath, OutputFolderPath);

                // Notify that the decompression is complete.
                await OnDecompressionComplete.InvokeAsync(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during decompression: {ex.Message}");
            }
        }
    }
}