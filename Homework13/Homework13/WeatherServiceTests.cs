using Moq;
using Xunit;
using System.Threading.Tasks;
using System;

namespace Homework13
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetWeatherData_ReturnsCorrectData()
        {
            var mockApiClient = new Mock<WeatherApiClient2>();

            var mockWeatherResponse = new WeatherResponse
            {
                location = new Location { name = "New York", country = "USA" },
                current = new CurrentWeather { tempc = 5f, windkph = 10f }
            };

            mockApiClient.Setup(x => x.GetWeatherDataAsync("New York")).ReturnsAsync(mockWeatherResponse);

            var weatherService = new WeatherService2(mockApiClient.Object);

            var result = await weatherService.GetWeatherDataAsync("New York");

            Assert.Equal("Weather Data for New York. Temperature: 5°C. Wind Speed: 10 km/h.", result);
        }

        [Fact]
        public async Task GetWeatherData_ReturnsNoData()
        {
            var mockApiClient = new Mock<WeatherApiClient2>();

            var mockWeatherResponse = new WeatherResponse
            {
                location = new Location { name = "New York", country = "USA" },
                current = null 
            };

            mockApiClient.Setup(x => x.GetWeatherDataAsync("New York")).ReturnsAsync(mockWeatherResponse);

            var weatherService = new WeatherService2(mockApiClient.Object);

            var result = await weatherService.GetWeatherDataAsync("New York");

            Assert.Equal("Weather data not available.", result);
        }

        [Fact]
        public async Task GetWeatherData_ReturnsApiFail()
        {
            var mockApiClient = new Mock<WeatherApiClient2>();

            mockApiClient.Setup(x => x.GetWeatherDataAsync("New York")).ThrowsAsync(new InvalidOperationException("API fail"));

            var weatherService = new WeatherService2(mockApiClient.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await weatherService.GetWeatherDataAsync("New York"));

            Assert.Equal("Error", ex.Message);
        }
    }
}
