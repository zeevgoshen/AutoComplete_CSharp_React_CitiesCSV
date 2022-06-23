//using ArrayTries;
using Microsoft.AspNetCore.Mvc;
using SailPoint_AutoComplete_ZG.Constants;
using SailPoint_AutoComplete_ZG.Data;
using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;
using System.Text.Json;
using TriesLib;


namespace SailPoint_AutoComplete_ZG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionsController : Controller
    {
        private readonly ILogger<SuggestionsController> _logger;

        public SuggestionsController(ILogger<SuggestionsController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public IEnumerable<CitiesModel> Post([FromBody] JsonElement text)
        {
            List<string> allCitiesStrings;
            Trie? trie = null;
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(text);
            string searchString = dict["text"].ToString();
            searchString = searchString.ToLowerInvariant();

            // ************************************************************************
            // When the app first loads (Home.js), the full list is saved in the cache.
            // ************************************************************************

            allCitiesStrings = CacheManager.Instance.GetAllCitiesStringList();

            trie = CacheManager.Instance.RetrieveTrie();
            
            if (trie == null) {
                trie = new Trie();

                int j = 0;
                foreach (string city in allCitiesStrings)
                {
                    if (city != null)
                    {
                        trie.Insert(city.ToLowerInvariant(), j);
                        j++;
                    }
                }

                // Save trie in cache
                CacheManager.Instance.SaveTrie(trie);

            }

            return SendResultsList(searchString, trie, allCitiesStrings);
        }

        public static List<CitiesModel> SendResultsList(string prefix, Trie trie, List<string> allCitiesStrings)
        {
            List<CitiesModel> result = new List<CitiesModel>();

            if (trie == null)
            {
                return null;
            }

            List<int> indices = trie.Collect(prefix);

            CitiesModel city;
            if (indices.Count > 0)
            {
                foreach (int i in indices)
                {
                    city = new CitiesModel(allCitiesStrings[i]);
                    city.Id = i;
                    result.Add(city);
                }
            }
            else
            {
                city = new CitiesModel(Messages.NO_CITY_FOUND);
                city.Id = 0;
                result.Add(city);
            }
            return result;
        }
    }
}