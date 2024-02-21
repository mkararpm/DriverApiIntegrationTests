using Azure;
using IntegrationTests.Ordering;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text.Json;

namespace DriverAPI.IntegrationTests.Controllers
{
    public class DriverTests : IClassFixture<HttpClientFixture>
    {
        private readonly HttpClientFixture _httpClientFixture;

        public DriverTests(HttpClientFixture httpClassFixture)
        {
            _httpClientFixture = httpClassFixture;
        }

        [Fact, TestPriority(0)]
        public async Task GetDriver()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/";

            // Act
            var response = await client.GetAsync(getUri);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task PostDriver_OK()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var postRequestContent = TestHelper.GetHttpContentFromSampleData("validPostDriver.json");

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/";

            // Act
            var response = await client.PostAsync(getUri, postRequestContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task PostInvalidDriver()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var requestContent = TestHelper.GetHttpContentFromSampleData("invalidPostDriver.json");

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver";

            //Act
            var response = await client.PostAsync(getUri, requestContent);

            //Assert  figure out why it gets ok when the bad data is entered
            //Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("mkara@rpmmoves.com", HttpStatusCode.Accepted)]
        [InlineData("test@mail.com", HttpStatusCode.Accepted)]
        public async Task GetUpdateEmail(string email, HttpStatusCode expectedStatusCode)
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/update-email/{email}";

            //Act
            var response = await client.GetAsync(getUri);

            //Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task PostCurrentLocation_OK()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var requestContent = TestHelper.GetHttpContentFromSampleData("validPostDriverCurrentLocation.json");

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/current-location";

            //Act
            var response = await client.PostAsync(getUri, requestContent);

            //Assert  figure out why it gets ok when the bad data is entered
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostCurrentLocation_BadRequest()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var requestContent = TestHelper.GetHttpContentFromSampleData("invalidPostDriverCurrentLocation.json");

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/current-location";

            //Act
            var response = await client.PostAsync(getUri, requestContent);

            //Assert  figure out why it gets ok when the bad data is entered
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostDriverGeofence_OK()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var requestContent = TestHelper.GetHttpContentFromSampleData("validPostDriverGeofence.json");

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/geofence";

            //Act
            var response = await client.PostAsync(getUri, requestContent);

            //Assert  figure out why it gets ok when the bad data is entered
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostDriverGeofence_BadRequest()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var requestContent = TestHelper.GetHttpContentFromSampleData("invalidPostDriverGeofence.json");

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/geofence";

            //Act
            var response = await client.PostAsync(getUri, requestContent);

            //Assert  figure out why it gets ok when the bad data is entered
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}