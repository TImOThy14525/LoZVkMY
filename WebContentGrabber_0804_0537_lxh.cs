// 代码生成时间: 2025-08-04 05:37:00
 * It includes error handling and follows C# best practices for maintainability and scalability.
 */
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWebContentGrabber
{
    // Razor component that uses the service
    public partial class WebContentGrabberComponent
    {
        [Inject]
        private IWebContentGrabberService WebContentGrabber { get; set; }

        // URL input field
        private string Url { get; set; } = "";

        // Method to invoke the content grabbing process
        public async Task GrabContentAsync()
        {
            try
            {
                string content = await WebContentGrabber.GetContentAsync(Url);
                // Display the fetched content or handle it as needed
                // For example, display it in a modal or a dedicated UI component
            }
            catch (Exception ex)
            {
                // Handle exceptions such as network errors, invalid URLs, etc.
                // Alert the user or log the error as needed
            }
        }
    }

    // Service for grabbing web content
    public interface IWebContentGrabberService
    {
        Task<string> GetContentAsync(string url);
    }

    public class WebContentGrabberService : IWebContentGrabberService
    {
        private readonly HttpClient _httpClient;

        public WebContentGrabberService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> GetContentAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                // Handle specific HTTP request exceptions
                throw new Exception("An error occurred while trying to fetch the web content.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions that may occur
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
