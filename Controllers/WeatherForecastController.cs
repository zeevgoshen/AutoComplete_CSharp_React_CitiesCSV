using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Web;

namespace SailPoint_AutoComplete_ZG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        List<CitiesModel> allCities;


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            
        }

        public List<CitiesModel> ReadCSVFile()
        {
            string[] paths = { @Environment.CurrentDirectory, @"Data\world-cities_csv.csv" };
            string fullPath = Path.Combine(paths);

            StreamReader reader = new StreamReader(System.IO.File.OpenRead(fullPath));

            var citiesList = new List<CitiesModel>();
            string row;

            CitiesModel city;

            while ((row = reader.ReadLine()) != null)
            {
                city = new CitiesModel(row);
                
                citiesList.Add(city);
    
            }
            reader.Close();

            

            return citiesList;
        }


        public List<CitiesModel> GetAllCities()
        {
            List<CitiesModel>? allCities = System.Runtime.Caching.MemoryCache.Default["names"] as List<CitiesModel>;
            if (allCities == null)
            {
                allCities = ReadCSVFile();
                System.Runtime.Caching.MemoryCache.Default["names"] = allCities;
                
            }

            return allCities;
        }

         
 


        [HttpGet]
        public IEnumerable<CitiesModel> Get()
        {
            return GetAllCities();

        }
    }
}