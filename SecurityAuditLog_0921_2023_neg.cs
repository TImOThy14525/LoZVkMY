// 代码生成时间: 2025-09-21 20:23:59
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace SecurityAuditApp
{
    /// <summary>
    /// 这是一个用于安全审计日志的Blazor组件。
    /// </summary>
    public class SecurityAuditLog : ComponentBase
    {
        /// <summary>
        /// 记录审计日志的文件路径。
        /// </summary>
        [Parameter]
        public string LogFilePath { get; set; }

        /// <summary>
        /// 审计日志容器。
        /// </summary>
        private List<string> auditLogs = new List<string>();

        /// <summary>
        /// 组件初始化时读取日志文件内容。
        /// </summary>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadAuditLogs();
            }
        }

        /// <summary>
        /// 加载审计日志文件内容。
        /// </summary>
        /// <returns>Task</returns>
        private async Task LoadAuditLogs()
        {
            try
            {
                if (!string.IsNullOrEmpty(LogFilePath) && File.Exists(LogFilePath))
                {
                    var lines = await File.ReadAllLinesAsync(LogFilePath);
                    auditLogs.AddRange(lines);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading audit logs: " + ex.Message);
            }
        }

        /// <summary>
        /// 添加一条新的审计日志记录。
        /// </summary>
        /// <param name="message">要记录的消息。</param>
        public void AddAuditLog(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    File.AppendAllText(LogFilePath, message + Environment.NewLine);
                    auditLogs.Add(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding audit log: " + ex.Message);
            }
        }

        /// <summary>
        /// 渲染审计日志。
        /// </summary>
        /// <returns>RenderFragment</returns>
        public RenderFragment RenderAuditLogs()
        {
            return builder =>
            {
                foreach (var log in auditLogs)
                {
                    builder.AddContent(0, $"<div>{log}</div>");
                }
            };
        }
    }
}