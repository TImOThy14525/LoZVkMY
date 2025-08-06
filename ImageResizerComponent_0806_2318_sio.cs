// 代码生成时间: 2025-08-06 23:18:32
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageBatchResizer.Components
{
    /// <summary>
    /// A Blazor component to batch resize images.
    /// </summary>
    public partial class ImageResizerComponent
    {
        [Parameter]
        public string[] ImagePaths { get; set; } = Array.Empty<string>();

        [Parameter]
        public int TargetWidth { get; set; } = 800;

        [Parameter]
        public int TargetHeight { get; set; } = 600;

        /// <summary>
        /// Triggers the batch resizing process.
        /// </summary>
        public async Task ResizeImagesAsync()
        {
            if (ImagePaths == null || ImagePaths.Length == 0)
            {
                Console.WriteLine("No images provided for resizing.");
                return;
            }

            foreach (var path in ImagePaths)
            {
                try
                {
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"Image not found at path: {path}");
                        continue;
                    }

                    await ResizeImageAsync(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing image at {path}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Resizes a single image.
        /// </summary>
        /// <param name="imagePath">The path to the image.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task ResizeImageAsync(string imagePath)
        {
            using var image = Image.Load(imagePath);
            image.Mutate(x => x.Resize(TargetWidth, TargetHeight));

            var outputPath = Path.Combine(Path.GetDirectoryName(imagePath), $"resized_{Path.GetFileName(imagePath)}");
            await image.SaveAsPngAsync(outputPath);

            Console.WriteLine($"Image resized and saved to {outputPath}");
        }
    }
}