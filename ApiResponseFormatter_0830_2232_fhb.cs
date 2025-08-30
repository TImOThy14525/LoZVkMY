// 代码生成时间: 2025-08-30 22:32:16
// 程序文件：ApiResponseFormatter.cs
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ApiFormatterApp
{
    /// <summary>
    /// API响应格式化工具
    /// </summary>
    public class ApiResponseFormatter
    {
        private readonly IJSRuntime jsRuntime;
# 优化算法效率

        /// <summary>
        /// 构造函数
        /// </summary>
# TODO: 优化性能
        /// <param name="jsRuntime">JavaScript运行时环境</param>
        public ApiResponseFormatter(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// 格式化API响应
        /// </summary>
# 改进用户体验
        /// <param name="response">API响应内容</param>
        /// <returns>格式化后的API响应</returns>
        public async Task<string> FormatApiResponseAsync(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
# 添加错误处理
            {
# FIXME: 处理边界情况
                throw new ArgumentException("API响应内容不能为空", nameof(response));
            }

            try
            {
                // 使用JsonSerializer.Deserialize将响应内容转换为Dictionary
                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
# NOTE: 重要实现细节
                if (data == null)
                {
                    throw new InvalidOperationException("解析响应失败");
                }

                // 调用JavaScript函数进行格式化
                return await jsRuntime.InvokeAsync<string>("apiResponseFormatter.format", data);
            }
            catch (JsonException ex)
            {
                throw new ApplicationException("JSON解析错误", ex);
            }
# 添加错误处理
        }
    }
}
