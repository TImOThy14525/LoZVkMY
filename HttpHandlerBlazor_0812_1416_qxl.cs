// 代码生成时间: 2025-08-12 14:16:07
// HttpHandlerBlazor.cs

using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp.Http
{
    // 该服务负责处理HTTP请求
    public class HttpHandler
    {
        private readonly HttpClient _httpClient;

        // 使用HttpClient依赖注入来初始化服务
        public HttpHandler(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // 异步发送GET请求并返回结果
        public async Task<string> GetAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                // 处理HTTP请求异常
                Console.WriteLine($"An error occurred while making a request: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        // 异步发送POST请求并返回结果
        public async Task<string> PostAsync(string url, string content)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(content)
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                // 处理HTTP请求异常
                Console.WriteLine($"An error occurred while making a request: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
    }

    // HttpHandler组件，用于调用HttpHandler服务
    public partial class HttpHandlerComponent : ComponentBase
    {
        [Inject]
        private HttpHandler HttpHandler { get; set; }

        private string _result;
        private string _url;
        private string _content;

        protected override async Task OnInitializedAsync()
        {
            // 异步调用GetAsync方法获取数据
            _result = await HttpHandler.GetAsync(_url);
        }

        // 发送GET请求
        private async Task GetRequest()
        {
            _result = await HttpHandler.GetAsync(_url);
            StateHasChanged();
        }

        // 发送POST请求
        private async Task PostRequest()
        {
            _result = await HttpHandler.PostAsync(_url, _content);
            StateHasChanged();
        }
    }
}
