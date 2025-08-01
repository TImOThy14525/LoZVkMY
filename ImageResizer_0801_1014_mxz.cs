// 代码生成时间: 2025-08-01 10:14:22
using Microsoft.AspNetCore.Components;
using SkiaSharp;
using System.IO;
using System.Threading.Tasks;

namespace ImageBatchResizer
{
    /// <summary>
    /// Component responsible for batch resizing images.
    /// </summary>
    public partial class ImageResizer
    {
        [Parameter]
        public string DirectoryPath { get; set; } = "";

        [Parameter]
        public int Width { get; set; } = 100;

        [Parameter]
        public int Height { get; set; } = 100;

        [Parameter]
        public string OutputDirectory { get; set; } = "";

        private bool isProcessing = false;
        private string statusMessage = "";

        /// <summary>
        /// Triggers the batch resizing of images.
        /// </summary>
        public async Task ResizeImagesAsync()
        {
            if (string.IsNullOrEmpty(DirectoryPath) || string.IsNullOrEmpty(OutputDirectory))
            {
                statusMessage = "Both directory and output directory must be provided.";
                return;
            }

            if (!Directory.Exists(DirectoryPath))
            {
                statusMessage = "Source directory does not exist.";
                return;
            }

            if (!Directory.Exists(OutputDirectory))
            {
                Directory.CreateDirectory(OutputDirectory);
            }

            statusMessage = "Processing images...";
            isProcessing = true;
            await ProcessImagesAsync();
            isProcessing = false;
            statusMessage = "Images processed successfully.";
        }

        /// <summary>
        /// Processes the images, resizing them to the specified dimensions.
        /// </summary>
        private async Task ProcessImagesAsync()
        {
            var files = Directory.GetFiles(DirectoryPath, "*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    try
                    {
                        var resizeTask = ResizeImageAsync(file);
                        await resizeTask;
                    }
                    catch (Exception ex)
                    {
                        statusMessage = $"Error processing {Path.GetFileName(file)}: {ex.Message}";
                    }
                }
            }
        }

        /// <summary>
        /// Resizes an individual image to the specified dimensions.
        /// </summary>
        private async Task ResizeImageAsync(string filePath)
        {
            using (var inputStream = File.OpenRead(filePath))
            {
                using (var bitmap = SKBitmap.Decode(inputStream))
                {
                    using (var resizedBitmap = SKImage.FromBitmap(bitmap.Resize(new SKImageInfo(Width, Height), SKFilterQuality.High)))
                    {
                        var outputPath = Path.Combine(OutputDirectory, Path.GetFileName(filePath));
                        using (var outputStream = File.OpenWrite(outputPath))
                        {
                            resizedBitmap.Encode(SKEncodedImageFormat.Jpeg, 100).SaveTo(outputStream);
                        }
                    }
                }
            }
        }
    }
}
