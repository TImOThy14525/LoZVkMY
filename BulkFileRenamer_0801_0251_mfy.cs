// 代码生成时间: 2025-08-01 02:51:46
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BulkFileRenamerApp
{
    // 组件类，用于批量文件重命名
    public partial class BulkFileRenamer
    {
        [Parameter]
        public string SourceFolder { get; set; } = string.Empty;

        [Parameter]
        public string TargetFolder { get; set; } = string.Empty;

        [Parameter]
        public string NamingPattern { get; set; } = "NewName_{0}"; // 默认命名模式

        private bool isRenaming;
        private string statusMessage;

        // 触发重命名操作的方法
        private async Task RenameFilesAsync()
        {
            if (string.IsNullOrWhiteSpace(SourceFolder) || string.IsNullOrWhiteSpace(TargetFolder))
            {
                statusMessage = "Source and target folders must be specified.";
                return;
            }

            if (!Directory.Exists(SourceFolder))
            {
                statusMessage = $"Source folder '{SourceFolder}' does not exist.";
                return;
            }

            if (!Directory.Exists(TargetFolder))
            {
                statusMessage = $"Target folder '{TargetFolder}' does not exist.";
                return;
            }

            isRenaming = true;
            statusMessage = "Starting rename process...";

            try
            {
                // 获取源文件夹中所有文件
                var files = Directory.GetFiles(SourceFolder);
                for (int i = 0; i < files.Length; i++)
                {
                    var oldPath = files[i];
                    var newFileName = string.Format(NamingPattern, i);
                    var newPath = Path.Combine(TargetFolder, newFileName + Path.GetExtension(oldPath));

                    // 重命名文件
                    File.Move(oldPath, newPath);
                }

                statusMessage = "Rename process completed successfully.";
            }
            catch (Exception ex)
            {
                statusMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                isRenaming = false;
            }
        }

        // 渲染方法
        private void RenderRenameButton()
        {
            // 按钮渲染逻辑
        }

        // 组件的OnAfterRenderAsync方法，用于异步操作后更新状态
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(() => StateHasChanged());
            }
        }
    }
}
