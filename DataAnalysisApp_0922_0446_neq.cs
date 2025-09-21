// 代码生成时间: 2025-09-22 04:46:26
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DataAnalysisApp
{
    // DataAnalysisApp组件负责显示数据分析界面和处理数据
    public partial class DataAnalysisApp
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        // 用于存储分析结果
        private string analysisResult = "";

        // 处理数据并展示分析结果的方法
        public async Task AnalyzeDataAsync(IEnumerable<double> data)
        {
            try
            {
                // 检查数据是否为空
                if (data == null || !data.GetEnumerator().MoveNext())
                {
                    Console.WriteLine("No data provided for analysis.");
                    return;
                }

                // 调用JavaScriptInterop来处理数据
                analysisResult = await JSRuntime.InvokeAsync<string>("dataAnalysis", data);

                // 刷新页面以显示结果
                StateHasChanged();
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"An error occurred during data analysis: {ex.Message}");
                analysisResult = "Error during analysis";
                StateHasChanged();
            }
        }

        // 渲染组件的UI部分
        private void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "data-analysis-app");
            builder.AddContent(2, $"Analysis Result: {analysisResult}");
            builder.CloseElement();
        }
    }
}
