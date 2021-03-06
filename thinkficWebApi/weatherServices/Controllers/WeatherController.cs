using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using weatherServices.Filters;
using weatherServices.Models;
using weatherServices.Services;

namespace weatherServices.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    //[ApiKeyAuth]
    public class WeatherController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly WeatherApiDbContext context;

        public WeatherController(IConfiguration configuration, WeatherApiDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        [HttpGet(Name = nameof(GetWeather))]
        public async Task<WeatherModel> GetWeather(string city)
        {
            var weatherService = new WeatherServices(city, configuration);
            var weather = await weatherService.WeatherRequest();

            // Add selective content from the API call into the InMemory Database
            if (!context.Weather.Any(w => w.Id == weather.Weather.First().Id))
            {
                context.Weather.Add(new WeatherEntity
                {
                    Id = weather.Weather.First().Id,
                    Main = weather.Weather.First().Main,
                    Description = weather.Weather.First().Description
                });

                await context.SaveChangesAsync();
            }

            return weather;
        }
    }
}
