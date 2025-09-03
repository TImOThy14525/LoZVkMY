// 代码生成时间: 2025-09-03 14:31:45
 * Includes error handling and documentation for maintainability and extensibility.
 */

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebScraperApp
{
    public partial class WebScraperBlazorApp
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private string scrapedContent = "";

        /// <summary>
        /// Asynchronously scrapes the content of a given URL.
        /// </summary>
        /// <param name="url">The URL to scrape content from.</param>
        /// <returns></returns>
        public async Task ScrapeContentAsync(string url)
        {
            try
            {
                // Use HttpClient to fetch the web content
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Read the content as a string
                    scrapedContent = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException e)
            {
                // Handle any web request errors
                scrapedContent = $"Error: {e.Message}";
            }
            catch (Exception e)
            {
                // Handle any other exceptions
                scrapedContent = $"Unexpected error: {e.Message}";
            }
        }

        /// <summary>
        /// Invokes the scraping method from the Blazor component.
        /// </summary>
        /// <param name="url">The URL to scrape content from.</param>
        public void OnScrapeButtonClick(string url)
        {
            ScrapeContentAsync(url);
        }
    }
}
