// 代码生成时间: 2025-09-09 23:01:18
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace FolderOrganizer
{
    // 一个用于整理文件夹结构的组件
    public partial class FolderOrganizer
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        // 选中文件夹的路径
        private string selectedFolderPath { get; set; }

        // 文件和文件夹的列表
        private List<FileSystemInfo> items = new List<FileSystemInfo>();

        // 组件初始化时执行的方法
        protected override void OnInitialized()
        {
            selectedFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            LoadItems(selectedFolderPath);
        }

        // 加载目录项的方法
        private void LoadItems(string path)
        {
            try
            {
                items = new DirectoryInfo(path).GetFileSystemInfos().ToList();
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error loading items: {ex.Message}");
            }
        }

        // 切换到选定的目录项
        private void EnterItem(FileSystemInfo item)
        {
            if (item is DirectoryInfo directory)
            {
                selectedFolderPath = directory.FullName;
                LoadItems(selectedFolderPath);
            }
        }

        // 渲染目录项的方法
        private RenderFragment RenderItem()
        {
            return builder =>
            {
                foreach (var item in items)
                {
                    if (item is DirectoryInfo directory)
                    {
                        builder.OpenComponent(0, typeof(Button));
                        builder.AddAttribute(1, "onclick", EventCallback.Factory.Create(this, () => EnterItem(item)));
                        builder.AddContent(2, directory.Name);
                        builder.CloseComponent();
                    }
                    else if (item is FileInfo file)
                    {
                        builder.OpenElement(3, "li");
                        builder.AddContent(4, file.Name);
                        builder.CloseElement();
                    }
                }
            };
        }
    }
}
