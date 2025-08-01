// 代码生成时间: 2025-08-02 03:42:11
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FileBackupAndSyncTool
{
    public class FileBackupAndSyncService
    {
        private readonly ILogger<FileBackupAndSyncService> _logger;

        public FileBackupAndSyncService(ILogger<FileBackupAndSyncService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Backups the specified source file to the backup directory.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="backupDirectory">The backup directory.</param>
        public async Task BackupFileAsync(string sourceFilePath, string backupDirectory)
        {
            try
            {
                string backupFilePath = Path.Combine(backupDirectory, Path.GetFileName(sourceFilePath));

                // Check if source file exists
                if (!File.Exists(sourceFilePath))
                {
                    _logger.LogError("Source file does not exist: {SourceFilePath}", sourceFilePath);
                    return;
                }

                // Create backup directory if it does not exist
                Directory.CreateDirectory(backupDirectory);

                // Copy source file to backup directory
                await File.CopyAsync(sourceFilePath, backupFilePath, overwrite: true);
                _logger.LogInformation("File backed up successfully: {BackupFilePath}", backupFilePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while backing up file: {SourceFilePath}", sourceFilePath);
                throw;
            }
        }

        /// <summary>
        /// Syncs the files between the source directory and the target directory.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="targetDirectory">The target directory.</param>
        public async Task SyncFilesAsync(string sourceDirectory, string targetDirectory)
        {
            try
            {
                // Create target directory if it does not exist
                Directory.CreateDirectory(targetDirectory);

                // Get all files in source directory
                var sourceFiles = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories);

                // Iterate through all source files and sync them to target directory
                foreach (var file in sourceFiles)
                {
                    string targetFilePath = Path.Combine(targetDirectory, file.Substring(sourceDirectory.Length + 1));
                    string targetDirectoryPath = Path.GetDirectoryName(targetFilePath);

                    // Create target directory if it does not exist
                    Directory.CreateDirectory(targetDirectoryPath);

                    // Copy file to target directory, overwrite if exists
                    await File.CopyAsync(file, targetFilePath, overwrite: true);
                    _logger.LogInformation("File synced successfully: {TargetFilePath}", targetFilePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while syncing files: {SourceDirectory}", sourceDirectory);
                throw;
            }
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("FileBackupAndSyncTool", LogLevel.Information)
                       .AddConsole();
            });

            ILogger<FileBackupAndSyncService> logger = loggerFactory.CreateLogger<FileBackupAndSyncService>();

            FileBackupAndSyncService service = new FileBackupAndSyncService(logger);

            string sourceFilePath = "path/to/source/file.txt";
            string backupDirectory = "path/to/backup";
            await service.BackupFileAsync(sourceFilePath, backupDirectory);

            string sourceDirectory = "path/to/source/directory";
            string targetDirectory = "path/to/target/directory";
            await service.SyncFilesAsync(sourceDirectory, targetDirectory);
        }
    }
}