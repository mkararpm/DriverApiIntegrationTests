
using Azure;
using IntegrationTests.Ordering;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text.Json;

namespace DriverAPI.IntegrationTests.Controllers
{
    public class MobileTests : IClassFixture<HttpClientFixture>
    {
        private readonly HttpClientFixture _httpClientFixture;

        public MobileTests(HttpClientFixture httpClassFixture)
        {
            _httpClientFixture = httpClassFixture;
        }

        [Fact]
        public async Task PostReportCrash()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var postContent = TestHelper.GetHttpContentFromSampleData("validPostMobileReportCrash.json");

            string getUri = $"{_httpClientFixture.BaseUrl}/api/mobile/report/crash";

            // Act
            var response = await client.PostAsync(getUri, postContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostReportError()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var postRequestContent = TestHelper.GetHttpContentFromSampleData("validPostMobileReportError.json");

            string getUri = $"{_httpClientFixture.BaseUrl}/api/mobile/report/error";

            // Act
            var response = await client.PostAsync(getUri, postRequestContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAnylineScretKey()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();

            string getUri = $"{_httpClientFixture.BaseUrl}/api/mobile/anyline-secretkey";

            //Act
            var response = await client.GetAsync(getUri);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetVideoTutorial()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string getUri = $"{_httpClientFixture.BaseUrl}/api/mobile/video-tutorial";

            //Act
            var response = await client.GetAsync(getUri);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAiagCodes()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string getUri = $"{_httpClientFixture.BaseUrl}/api/mobile/aiag-codes";

            //Act
            var response = await client.GetAsync(getUri);

            //Assert  figure out why it gets ok when the bad data is entered
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}