// 代码生成时间: 2025-09-17 20:38:50
// JsonDataConverter.cs
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace JsonDataConverterApp
{
    // 组件类，用于在Blazor应用中显示和转换JSON数据
    public partial class JsonDataConverter
    {
        [Parameter]
        public string JsonInput { get; set; } = ""; // 输入的JSON字符串

        [Parameter]
        public EventCallback<string> OnJsonConverted { get; set; } // JSON转换后的回调事件

        private string convertedJson; // 转换后的JSON字符串

        // 将输入的JSON字符串转换为格式化的JSON字符串
        private async Task ConvertJson()
        {
            try
            {
                // 尝试反序列化JSON字符串为对象
                var obj = JsonSerializer.Deserialize<object>(JsonInput);
                if (obj != null)
                {
                    // 将对象序列化回格式化的JSON字符串
                    convertedJson = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });

                    // 触发回调事件，传递转换后的JSON字符串
                    await OnJsonConverted.InvokeAsync(convertedJson);
                }
                else
                {
                    // 输入的JSON字符串无效，不能反序列化
                    convertedJson = "Invalid JSON input";
                }
            }
            catch (JsonException)
            {
                // 输入的JSON字符串格式错误
                convertedJson = "Invalid JSON format";
            }
        }

        // 清除转换后的JSON字符串
        private void ClearJson()
        {
            convertedJson = "";
        }

        // 渲染组件的逻辑
        private void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "json-converter");

            builder.OpenElement(2, "textarea");
            builder.AddAttribute(3, "id", "jsonInput");
            builder.AddAttribute(4, "placeholder", "Enter JSON data");
            builder.AddAttribute(5, "onchange", EventCallback.Factory.Create<string>(this, ConvertJson));
            builder.AddAttribute(6, "value", JsonInput);
            builder.CloseElement();

            builder.OpenElement(7, "div");
            builder.AddAttribute(8, "class", "converted-json");
            builder.AddContent(9, convertedJson == null ? "" : convertedJson);
            builder.CloseElement();

            builder.CloseElement();
        }
    }
}