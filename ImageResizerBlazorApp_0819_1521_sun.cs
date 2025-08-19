// 代码生成时间: 2025-08-19 15:21:38
using Microsoft.AspNetCore.Components.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageResizerBlazorApp
{
    /*
    * 图片尺寸批量调整器组件
    * 提供用户界面来选择图片文件和输出尺寸，并展示处理结果。
    * 包含错误处理和文档注释，遵循CSHARP最佳实践。
    */
    public partial class ImageResizer
    {
        private IBrowserFile _selectedFile;
        private string _outputImage;
        private Image<Rgba32> _image;
        private FileInfo[] _inputFiles;
        private Image<Rgba32> _outputImageInstance;
        private int _width = 800; // 默认宽度
        private int _height = 600; // 默认高度
        private bool _resize; // 是否需要调整尺寸

        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            // 保存文件信息
            _inputFiles = e.FileList.Select(f => new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), f.Name))).ToArray();
            _selectedFile = e.FileList.First();

            try
            {
                // 读取图片文件
                using var stream = _selectedFile.OpenReadStream();
                _image = await Image.LoadAsync<Rgba32>(stream);
                _outputImage = Convert.ToBase64String(await _image.ToByteArrayAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
                // 错误处理
                _outputImage = null;
            }
        }

        private async Task ResizeImage()
        {
            if (_selectedFile == null || !_resize) return;

            try
            {
                // 读取图片文件
                using var stream = _selectedFile.OpenReadStream();
                _image = await Image.LoadAsync<Rgba32>(stream);
                _outputImageInstance = await _image.CloneAsync();

                // 调整图片尺寸
                _outputImageInstance.Mutate(x => x.Resize(_width, _height));

                // 转换为Base64字符串
                _outputImage = Convert.ToBase64String(await _outputImageInstance.ToByteArrayAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resizing image: {ex.Message}");
                // 错误处理
                _outputImage = null;
            }
        }
    }
}
