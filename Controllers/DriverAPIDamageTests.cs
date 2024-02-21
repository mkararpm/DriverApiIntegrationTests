using IntegrationTests.Ordering;
using Newtonsoft.Json;
using System.Net;
using System.Net.WebSockets;
using System.Text.Json;

namespace DriverAPI.IntegrationTests.Controllers
{
    public class DriverAPIDamageTests : IClassFixture<HttpClientFixture>
    {
        private readonly HttpClientFixture _httpClientFixture;

        public DriverAPIDamageTests(HttpClientFixture httpClassFixture)
        {
            _httpClientFixture = httpClassFixture;
        }

        [Fact]
        public async Task GetDamagewithRouteIDAndVin()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string routeID = "1168802";
            string vin = "1FDWE35SX5HA40825";

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/shipment/routeId/{routeID}/vin/{vin}/damages";

            // Act
            var response = await client.GetAsync(getUri);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetDamagewithRouteIDAndVinBadRequest()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string routeID = "116880245";
            string vin = "1FDWE35SX5HA49111";

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/shipment/routeId/{routeID}/vin/{vin}/damages";

            // Act
            var response = await client.GetAsync(getUri);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostDamagewithRouteIDAndVin()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var postRequestContent = TestHelper.GetHttpContentFromSampleData("validPostDamageShipment.json");

            string routeID = "1168802";
            string vin = "1MEBP88U0GG643233";

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/shipment/routeId/{routeID}/vin/{vin}/damages";

            // Act
            var response = await client.PostAsync(getUri, postRequestContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetShipmentCountDamages()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string routeID = "1168802";
            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/shipment/routeId/{routeID}/count-damages";

            // Act
            var response = await client.GetAsync(getUri);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostShipmentCountDamages()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            var postRequestContent = TestHelper.GetHttpContentFromSampleData("validPostShipmentCountDamages.json");

            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/shipment/count-damages-damages";

            // Act
            var response = await client.PostAsync(getUri, postRequestContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetShipmentDamageCodes()
        {
            //Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/shipment/damageCodes";

            //Act
            var response = await client.GetAsync(getUri);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task VerifyShipmentDamages()
        {
            // Arrange
            var client = await _httpClientFixture.GetAuthorizedHttpClientAsync();
            string getUri = $"{_httpClientFixture.BaseUrl}/api/driver/shipment/damageCodes";

            // Act
            var response = await client.GetAsync(getUri);

            string stringResponse = await response.Content.ReadAsStringAsync();
            string shipmentDamagesList = File.ReadAllText($"SampleData/shipmentDamageCodes.json");

            // Assert
            Assert.Equal(shipmentDamagesList.ToString(), stringResponse);
        }
    }
}