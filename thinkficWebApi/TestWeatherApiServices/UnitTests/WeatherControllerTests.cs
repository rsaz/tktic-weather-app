using System.Threading.Tasks;
using Xunit;
using System.Threading.Tasks;
using weatherServices.Services;
using Xunit;
using System;
using weatherServices.Extensions;
using Microsoft.AspNetCore.Diagnostics;

namespace TestWeatherApiServices
{
    public class WeatherControllerTests
    {
        [Fact]
        public async Task GetWeather_ReturnsUnAuthorizedAccessCode_FromWeatherApiCall()
        {
            // Arrange
            string cityQuery = "Vancouver";

            // Act
            var result = await WeatherServices.WeatherRequest(cityQuery);
            
            // Assert
            Assert.True(result.Name.Equals(cityQuery));

        }
    }
}
