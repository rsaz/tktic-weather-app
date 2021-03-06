using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace weatherServices.Controllers
{
    [Route("v{version:apiVersion}/")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(200)]
    public class RootController : Controller
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                weather = new
                {
                    href = Url.Link(nameof(WeatherController.GetWeather), null)
                }
            };
            return Ok(response);
        }
    }
}
