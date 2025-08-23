// 代码生成时间: 2025-08-23 17:22:28
/// <summary>
/// Represents the authentication service for handling user authentication in a Blazor application.
/// </summary>
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;

namespace BlazorApp.Authentication
{
    public class AuthenticationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _jsRuntime;

        /// <summary>
        /// Initializes a new instance of the AuthenticationService class.
        /// </summary>
        /// <param name="httpClientFactory">The factory used to create HttpClient instances.</param>
        /// <param name="navigationManager">The navigation manager for handling navigation within the application.</param>
        /// <param name="jsRuntime">The JavaScript runtime for interacting with JavaScript code.</param>
        public AuthenticationService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager, IJSRuntime jsRuntime)
        {
            _httpClientFactory = httpClientFactory;
            _navigationManager = navigationManager;
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Authenticates a user with the provided credentials.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task representing the asynchronous authentication operation.</returns>
        public async Task<AuthenticateResult> AuthenticateUserAsync(string username, string password)
        {
            try
            {
                // Create an HttpClient instance using the factory
                var httpClient = _httpClientFactory.CreateClient();

                // Define the API endpoint for authentication
                var endpoint = "api/authenticate";

                // Prepare the authentication request with the provided credentials
                var requestContent = new FormEncodedValueProvider(new Dictionary<string, string> {
                    { "username", username },
                    { "password", password }
                });
                var content = new FormEncodedValueProviderRequestContent(requestContent);

                // Send the authentication request to the server
                var response = await httpClient.PostAsync(endpoint, content);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to an AuthenticateResult object
                    var result = await response.Content.ReadFromJsonAsync<AuthenticateResult>();

                    // Return the AuthenticateResult object
                    return result;
                }
                else
                {
                    // Handle authentication failure and throw an exception
                    throw new InvalidOperationException("Authentication failed.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle any HTTP request exceptions
                throw new Exception("An error occurred during authentication.", ex);
            }
        }

        /// <summary>
        /// Represents the result of an authentication operation.
        /// </summary>
        public class AuthenticateResult
        {
            [JsonProperty("isAuthenticated")]
            public bool IsAuthenticated { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("token")]
            public string Token { get; set; }
        }
    }
}
