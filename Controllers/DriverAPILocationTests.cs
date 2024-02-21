using IntegrationTests.Ordering;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace DriverAPI.IntegrationTests.Controllers
{
    [TestCaseOrderer("IntegrationTests.Ordering.PriorityOrderer", "Integration.Tests")]
    public class DriverAPILocationTests : IClassFixture<HttpClientFixture>
    {
        private readonly HttpClientFixture _httpClientFixture;

        public DriverAPILocationTests(HttpClientFixture httpClassFixture)
        {
            _httpClientFixture = httpClassFixture;
        }

        [Fact]
        public async Task GetLocation()
        {
            // Arrange
            var client = new HttpClient();
            string getUri = $"{_httpClientFixture.BaseUrl}/api/countries/";

            // Act
            var response = await client.GetAsync(getUri);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task VerifyLocationCountries()
        {
            // Arrange
            var client = new HttpClient();
            string getUri = $"{_httpClientFixture.BaseUrl}/api/countries/";

            // Act
            var response = await client.GetAsync(getUri);

            string stringResponse = await response.Content.ReadAsStringAsync();
            string countriesList = File.ReadAllText($"Controllers/SampleData/Countries.json");

            // Assert
            Assert.Equal(stringResponse, countriesList.ToString());
        }
    }
}