using DemoDbUpdate.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using DemoDbUpdate.Server.Hubs;

namespace DemoDbUpdate.Server.Controllers
{
    //[ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private List<WeatherForecast> weatherForecasts = new List<WeatherForecast>();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHubContext<NotificationHub> hubContext)
        {
            this.logger = logger;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return MockDatabase.GetFromDatabase().ToArray();

        }

        [HttpGet("adddata")]
        public async Task<IActionResult> AddData()
        {
            var rng = new Random();
            var newData = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            MockDatabase.SaveToDatabase(newData);
            await _hubContext.Clients.All.SendAsync("datachanged");

            return Ok(string.Format("{0} - New data added to the server's database.", DateTime.Now.ToString()));
        }


    }
}
