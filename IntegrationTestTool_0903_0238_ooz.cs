// 代码生成时间: 2025-09-03 02:38:37
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.Modal;
using Blazored.Modal.Services;

namespace BlazorApplication
{
    // Represents the main application component
    public class IntegrationTestTool
    {
        [Inject]
        private IModalService ModalService { get; set; }

        [Inject]
        private IHttpClientFactory HttpClientFactory { get; set; }

        // Method to initiate a test
        public async Task RunTestAsync(string testUrl)
        {
            try
            {
                var httpClient = HttpClientFactory.CreateClient();
                var response = await httpClient.GetAsync(testUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    await ModalService.Show<ResultsModal>(modalParameters: new object[] { responseContent });
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"General exception: {e.Message}");
            }
        }
    }

    // Component for displaying test results
    public partial class ResultsModal
    {
        [Parameter]
        public string Result { get; set; }

        private async Task CloseModal()
        {
            await this.ModalService.CloseAsync(this);
        }
    }
}
