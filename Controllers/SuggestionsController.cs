using Microsoft.AspNetCore.Mvc;
using SailPoint_AutoComplete_ZG.Data;
using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace SailPoint_AutoComplete_ZG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionsController : ControllerBase
    {

        private readonly ILogger<SuggestionsController> _logger;

        public SuggestionsController(ILogger<SuggestionsController> logger)
        {
            _logger = logger;            
        }


        [HttpPost]
        public IEnumerable<CitiesModel> Post([FromBody] JsonElement text)
        {
            ConcurrentBag<CitiesModel> allCities = CacheManager.Instance.GetAllCities();

            //ConcurrentBag<CitiesModel> filteredCities = allCities.Select((items) => items.CityName.Contains(text.ToString()));


            return null;

        }
    }
}