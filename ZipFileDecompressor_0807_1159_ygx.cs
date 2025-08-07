// 代码生成时间: 2025-08-07 11:59:25
using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Components;

namespace ZipFileDecompressorApp
{
    /// <summary>
    /// A component that handles the decompression of zip files.
    /// </summary>
    public partial class ZipFileDecompressor
    {
        [Parameter]
        public string ZipFilePath { get; set; }

        [Parameter]
        public string DestinationDirectory { get; set; }

        [Parameter]
        public EventCallback OnDecompressionComplete { get; set; }

        private bool isDecompressing = false;

        private async Task DecompressZipFile()
        {
            if (!File.Exists(ZipFilePath))
            {
                Console.WriteLine("The zip file does not exist.");
                return;
            }

            if (isDecompressing)
            {
                Console.WriteLine("A decompression process is already in progress.");
                return;
            }

            try
            {
                isDecompressing = true;
                await using var fileStream = new FileStream(ZipFilePath, FileMode.Open);
                using var archive = new ZipArchive(fileStream, ZipArchiveMode.Read, true);
                await using var tempStream = new MemoryStream();

                foreach (var entry in archive.Entries)
                {
                    await using var entryStream = entry.Open();
                    await entryStream.CopyToAsync(tempStream);
                    tempStream.Position = 0;

                    string destinationPath = Path.Combine(DestinationDirectory, entry.FullName);
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    if (entry.Length == 0)
                    {
                        continue; // Skip directories.
                    }

                    await using var fileStreamOutput = new FileStream(destinationPath, FileMode.Create);
                    await tempStream.CopyToAsync(fileStreamOutput);
                }

                Console.WriteLine("Decompression completed successfully.");
                await OnDecompressionComplete.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during decompression: {ex.Message}");
            }
            finally
            {
                isDecompressing = false;
            }
        }
    }
}
