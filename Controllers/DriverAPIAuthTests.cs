using IntegrationTests.Ordering;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace DriverAPI.IntegrationTests.Controllers
{
    [TestCaseOrderer("IntegrationTests.Ordering.PriorityOrderer", "Integration.Tests")]
    public class DriverAPIAuthTests : IClassFixture<HttpClientFixture>
    {
        private readonly HttpClientFixture _httpClientFixture;

        public DriverAPIAuthTests(HttpClientFixture httpClassFixture)
        {
            _httpClientFixture = httpClassFixture;
        }

        [Fact]
        public async Task PostVerificationCode()
        {
            // Arrange
            var client = new HttpClient();
            var postRequestContent = TestHelper.GetHttpContentFromSampleData("authGetData.json");
            string getUri = $"{_httpClientFixture.BaseUrl}/api/auth/verification-code";

            // Act
            var createResponse = await client.PostAsync(getUri, postRequestContent);

            // Assert
            Assert.Equal(HttpStatusCode.Accepted, createResponse.StatusCode);
        }


        [Fact]
        public async Task ValidateVerificationCode()
        {
            // Arrange
            var client = new HttpClient();
            var postRequestContent = TestHelper.GetHttpContentFromSampleData("authGetData.json");
            string getUri = $"{_httpClientFixture.BaseUrl}/api/auth/validate-verification-code";

            // Act
            var createResponse = await client.PostAsync(getUri, postRequestContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);
        }
    }
}