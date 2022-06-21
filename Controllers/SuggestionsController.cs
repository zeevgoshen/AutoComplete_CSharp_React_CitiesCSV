//using ArrayTries;
using Microsoft.AspNetCore.Mvc;
using SailPoint_AutoComplete_ZG.Data;
using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;
using System.Text.Json;
using TriesLib;


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
            List<string> allCitiesStrings;// = new List<string>();
            Trie? trie = null;
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(text);
            string searchString = dict["text"].ToString();

            // **************************************************************
            // when the app first loads, the full list is saved in the cache.
            // so there is no searchString.Length == 0 case.
            //
            // first letter (prefix) has already been entered
            // **************************************************************

            allCitiesStrings = CacheManager.Instance.GetAllCitiesStringList();
            if (searchString.Length == 1 && trie == null)
            {
                trie = new Trie();

                for (int j = 0; j < allCitiesStrings.Count - 1; ++j)
                {
                    if (allCitiesStrings[j] != null)
                    {
                        trie.Insert(allCitiesStrings[j].ToLowerInvariant(), j);
                    }
                }
                // Save trie of first letter in cache
                CacheManager.Instance.SaveTrieOfFirstLetter(trie, searchString);

            } 
            else if (searchString.Length > 1)
            {
                trie = CacheManager.Instance.RetrieveTrieOfFirstLetter(searchString);
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
            } else
            {
                city = new CitiesModel("No such city.");
                city.Id = 0;
                result.Add(city);
            }
            return result;
        }
    }
}