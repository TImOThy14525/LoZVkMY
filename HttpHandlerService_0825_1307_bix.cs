// 代码生成时间: 2025-08-25 13:07:49
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorHttpRequestHandler
{
    /// <summary>
    /// Provides methods to handle HTTP requests.
    /// </summary>
    public class HttpHandlerService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the HttpHandlerService.
        /// </summary>
        /// <param name="httpClient">HttpClient instance.</param>
        public HttpHandlerService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Sends a GET request to the specified URL and returns the response as a JSON object.
        /// </summary>
        /// <typeparam name="T">The type of the JSON response.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <returns>The JSON response deserialized to the specified type T.</returns>
        public async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        /// <summary>
        /// Sends a POST request to the specified URL with the provided content and returns the response as a JSON object.
        /// </summary>
        /// <typeparam name="TRequest">The type of the JSON request content.</typeparam>
        /// <typeparam name="TResponse">The type of the JSON response.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="content">The JSON content to send in the request.</param>
        /// <returns>The JSON response deserialized to the specified type TResponse.</returns>
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest content)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        // Additional methods for PUT, DELETE, etc. can be added here following the same pattern.
    }
}
