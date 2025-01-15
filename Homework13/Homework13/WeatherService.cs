using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework13
{
    public interface WeatherApiClient2
    {
        Task<WeatherResponse> GetWeatherDataAsync(string city);
    }

    public class WeatherService2
    {
        private readonly WeatherApiClient2 _weatherApiClient;

        public WeatherService2(WeatherApiClient2 weatherApiClient)
        {
            _weatherApiClient = weatherApiClient;
        }

        public async Task<string> GetWeatherDataAsync(string city)
        {
            try
            {
                var weatherResponse = await _weatherApiClient.GetWeatherDataAsync(city);

                if (weatherResponse?.current != null)
                {
                    return $"Weather Data for {weatherResponse.location.name}. " +
                           $"Temperature: {weatherResponse.current.tempc}°C. " +
                           $"Wind Speed: {weatherResponse.current.windkph} km/h.";
                }
                return "Weather data not available.";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error", ex);
            }
        }
    }
}

