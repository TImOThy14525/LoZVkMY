// 代码生成时间: 2025-08-21 17:01:09
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiResponseFormatterApp
{
    // Component for formatting API responses
    public partial class ApiResponseFormatterApp
    {
        // Event callback for when a successful API response is received
        [Parameter]
        public EventCallback<HttpResponseMessage> OnApiResponseReceived { get; set; }

        // The API response data as a string for display purposes
        private string apiResponseData = "";

        // Asynchronously fetches data from the specified API URL
        private async Task FetchDataAsync(string apiUrl)
        {
            try
            {
                // Initialize the HttpClient for API requests
                using (HttpClient client = new HttpClient())
                {
                    // Send a GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Throw an exception if the response is not successful
                    response.EnsureSuccessStatusCode();

                    // Read the response content as a string
                    apiResponseData = await response.Content.ReadAsStringAsync();

                    // Invoke the event callback with the API response
                    await OnApiResponseReceived.InvokeAsync(response);
                }
            }
            catch (HttpRequestException e)
            {
                // Handle any HTTP request exceptions
                apiResponseData = $"Error fetching data: {e.Message}";
            }
            catch (JsonException e)
            {
                // Handle any JSON parsing exceptions
                apiResponseData = $"Error parsing JSON: {e.Message}";
            }
            catch (Exception e)
            {
                // Handle any other exceptions
                apiResponseData = $"An error occurred: {e.Message}";
            }
        }

        // Method to handle the button click event to fetch data
        private async Task OnButtonClick(string apiUrl)
        {
            await FetchDataAsync(apiUrl);

            // State has changed, so invoke StateHasChanged to update the UI
            StateHasChanged();
        }
    }
}
