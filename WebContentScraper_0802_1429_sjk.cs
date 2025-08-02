// 代码生成时间: 2025-08-02 14:29:40
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorWebContentScraper
{
    public class WebContentScraper
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        // 构造函数注入HttpClient和IJSRuntime
        public WebContentScraper(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }

        // 异步方法，用于抓取网页内容
        public async Task<string> ScrapeWebContentAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
            }

            try
            {
                // 发送HTTP GET请求
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException ex)
            {
                // 错误处理：捕获HTTP请求异常并抛出
                throw new Exception($"Failed to retrieve content from {url}. Error: {ex.Message}", ex);
            }
        }

        // 异步方法，用于将网页内容渲染到Blazor组件中
        public async Task RenderWebContentAsync(string url, string elementId)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
            }
            if (string.IsNullOrWhiteSpace(elementId))
            {
                throw new ArgumentException("Element ID cannot be null or whitespace.", nameof(elementId));
            }

            try
            {
                // 抓取网页内容
                var content = await ScrapeWebContentAsync(url);

                // 使用JSInterop将内容注入到指定的DOM元素中
                await _jsRuntime.InvokeVoidAsync("setContent", elementId, content);
            }
            catch (Exception ex)
            {
                // 错误处理：捕获并记录异常信息
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
