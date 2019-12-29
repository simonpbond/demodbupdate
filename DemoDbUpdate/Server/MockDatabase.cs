using DemoDbUpdate.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDbUpdate.Server
{
    public class MockDatabase
    {


        public static List<WeatherForecast> mockDatabaseWeatherForecast = new List<WeatherForecast>();


        public static void SaveToDatabase(IEnumerable<WeatherForecast> weatherForecasts)
        {
            mockDatabaseWeatherForecast.AddRange(weatherForecasts);
        }

        public static IEnumerable<WeatherForecast> GetFromDatabase()
        {
            return mockDatabaseWeatherForecast;
        }


    }
}
