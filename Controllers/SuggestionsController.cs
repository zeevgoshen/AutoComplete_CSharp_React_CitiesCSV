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
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(text);
            ConcurrentBag<CitiesModel> allCities = CacheManager.Instance.GetAllCities();
            string searchString = dict["text"].ToString();

            // Should store in cache the last search results, to search only in those.
            // empty cache when a new search starts ( the text is empty or or the first letter is different )


            Trie result = new Trie();

            //List<string> filteredCities = allCities.Where(a => a.CityName.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)).ToList();


            List<CitiesModel> filteredCities = allCities.Where(a => a.CityName.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            //result.InsertRange(filteredCities.ToList());
            
            return filteredCities;

        }
    }
}