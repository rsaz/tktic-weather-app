using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using weatherServices.Models;

namespace weatherServices.Services
{
    public class WeatherServices
    {
        private string _city;
        private static string _apiKey;

        public WeatherServices(string city, IConfiguration configuration)
        {               
            _city = city;
            _apiKey = configuration.GetSection("ApiKey").Value;           
        }

        public async Task<WeatherModel> WeatherRequest()
        {
            var weather = new WeatherModel();

            if (string.IsNullOrEmpty(_city))
            {
                throw new Exception("Exception trying to call 'openweathermapapi' ");
            }

            string apiCall = $"https://api.openweathermap.org/data/2.5/weather?q={_city}&units=metric&appid={_apiKey}";
            var apiRequest = WebRequest.Create(apiCall) as HttpWebRequest;
            if (apiRequest == null)
            {
                throw new Exception("Api bad request");
            }

            apiRequest.ContentType = "application/json";

            using (var content = (await apiRequest.GetResponseAsync()).GetResponseStream())
            {
                using (var stream = new StreamReader(content))
                {
                    var json = stream.ReadToEnd();
                    weather = JsonSerializer.Deserialize<WeatherModel>(json);
                }
            }

            return weather;
        }

        public static async Task<WeatherModel> WeatherRequest(string city)
        {
            var weather = new WeatherModel();

            if (string.IsNullOrEmpty(city))
            {
                throw new Exception("Exception trying to call 'openweathermapapi' ");
            }

            string apiCall = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=3d7a8228e71975cfbad0800ab1b88a16";
            var apiRequest = WebRequest.Create(apiCall) as HttpWebRequest;
            if (apiRequest == null)
            {
                throw new Exception("Api bad request");
            }

            apiRequest.ContentType = "application/json";

            using (var content = (await apiRequest.GetResponseAsync()).GetResponseStream())
            {
                using (var stream = new StreamReader(content))
                {
                    var json = stream.ReadToEnd();
                    weather = JsonSerializer.Deserialize<WeatherModel>(json);
                }
            }

            return weather;
        }
    }
}
