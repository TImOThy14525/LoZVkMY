// 代码生成时间: 2025-08-10 07:25:56
// <summary>
// This class is used to scrape content from a webpage using C# and Blazor framework.
// </summary>
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebScraper
{
    // Define a Blazor component to display the scraped content
    public class WebContentScraper : ComponentBase
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        // State to hold the scraped content
        private string scrapedContent;

        // Method to invoke JavaScript to scrape content from a webpage
        public async Task ScrapeContentAsync(string url)
        {
            try
            {
                // Call the JavaScript function to scrape content
                scrapedContent = await JSRuntime.InvokeAsync<string>("scrapeContent", url);

                // State has changed, so Blazor will re-render the component
                StateHasChanged();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the scraping process
                Console.WriteLine($"Error scraping content: {ex.Message}");
                scrapedContent = "Error scraping content.";
            }
        }

        // Render the scraped content in the Blazor component
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddContent(1, scrapedContent);
            builder.CloseElement();
        }
    }
}
