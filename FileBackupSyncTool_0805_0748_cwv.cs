// 代码生成时间: 2025-08-05 07:48:21
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileBackupSyncTool
{
    // 文件备份和同步工具
    public class FileBackupSyncTool
    {
        private readonly string _sourcePath;
        private readonly string _destinationPath;

        // 构造函数，初始化源路径和目标路径
        public FileBackupSyncTool(string sourcePath, string destinationPath)
        {
            _sourcePath = sourcePath;
            _destinationPath = destinationPath;
        }

        // 同步文件方法
        public async Task SyncFilesAsync()
        {
            try
            {
                // 确保源路径和目标路径存在
                Directory.CreateDirectory(_sourcePath);
                Directory.CreateDirectory(_destinationPath);

                // 获取源路径和目标路径中的文件信息
                var sourceFiles = Directory.GetFiles(_sourcePath);
                var destinationFiles = Directory.GetFiles(_destinationPath);

                // 遍历源路径中的文件
                foreach (var sourceFile in sourceFiles)
                {
                    var fileName = Path.GetFileName(sourceFile);
                    var destinationFile = Path.Combine(_destinationPath, fileName);

                    // 如果目标路径中不存在文件，则进行复制
                    if (!destinationFiles.Contains(destinationFile))
                    {
                        File.Copy(sourceFile, destinationFile, overwrite: true);
                    }
                }

                Console.WriteLine("Files have been synced successfully.");
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
