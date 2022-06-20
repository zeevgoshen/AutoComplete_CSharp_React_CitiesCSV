using Microsoft.AspNetCore.Mvc;
using SailPoint_AutoComplete_ZG.Data;
using SailPoint_AutoComplete_ZG.Logic.Models;


namespace SailPoint_AutoComplete_ZG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {

        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger;            
        }

        

        //public List<CitiesModel> GetAllCities()
        //{
        //    List<CitiesModel>? allCities = System.Runtime.Caching.MemoryCache.Default["names"] as List<CitiesModel>;
        //    if (allCities == null)
        //    {
        //        allCities = Utils.ReadCSVFile();
        //        System.Runtime.Caching.MemoryCache.Default["names"] = allCities;
                
        //    }

        //    return allCities;
        //}


        [HttpGet]
        public IEnumerable<CitiesModel> Get()
        {
            return CacheManager.Instance.GetAllCities();

        }
    }
}