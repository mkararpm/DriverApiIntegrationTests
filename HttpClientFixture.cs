using Azure;
using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DriverAPI.IntegrationTests
{
    public class HttpClientFixture : IDisposable
    {
        public AccessToken? Token { get; set; } = null;
        private HttpClient Client { get; } = new HttpClient();
        public string BaseUrl { get; set; }
        public string Scope { get; set; }
        public string CreatedId { get; set; }


        public HttpClientFixture()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var apiConfig = configuration.Get<ApiConfig>()!;
            BaseUrl = "https://staging-driverapi.loadrpm.com"; // apiConfig.BaseUrl;
            Scope = apiConfig.Scope;
            CreatedId = ""; // initially empty string, to be populated later
        }

        internal async Task<HttpClient> GetAuthorizedHttpClientAsync()
        {
            var jsonContent1 = TestHelper.GetHttpContentFromSampleData("authPrep.json");

            // We need to make this call 1st - in order for the 2nd call to retrieve an auth token
            string UriPrep = $"{BaseUrl}/api/auth/verification-code";
            var response1 = await Client.PostAsync(UriPrep, jsonContent1);

            // Get the auth token
            var jsonContent2 = TestHelper.GetHttpContentFromSampleData("authGetData.json");

            string UriAuthToken = $"{BaseUrl}/api/auth/validate-verification-code";
            var response2 = await Client.PostAsync(UriAuthToken, jsonContent2);

            var finalresponse = await response2.Content.ReadAsStringAsync();

            var token_response = JsonConvert.DeserializeObject<TokenResponse>(finalresponse);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_response.access_token);

            return Client;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Client.Dispose();
        }
    }
}
