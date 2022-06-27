//using ArrayTries;
using Microsoft.AspNetCore.Mvc;
using SailPoint_AutoComplete_ZG.Constants;
using SailPoint_AutoComplete_ZG.Data;
using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Text.Json;
using TriesLib;


namespace SailPoint_AutoComplete_ZG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionsController : Controller
    {
        private readonly ILogger<SuggestionsController> _logger;

        // the logger is not used but is still here for future use
        public SuggestionsController(ILogger<SuggestionsController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public async Task<List<CitiesModel>> Post([FromBody] JsonElement text)
        {
            List<string> allCitiesStrings;
            Trie? trie = null;

            try
            {
                var query = JsonSerializer.Deserialize<Dictionary<string, string>>(text);

                if (query != null && query.Count > 0)
                {
                    string searchString = query[Strings.QUERY_TEXT].ToString();
                    searchString = searchString.ToLowerInvariant();

                    // server side validations
                    if (searchString.Length == 0 || (!Utils.IsValidSearchText(searchString)))
                    {
                        throw new Exception(Strings.VALIDATION_ERR_MESSAGE);
                    }

                    allCitiesStrings = await CacheManager.Instance.GetAllCitiesStringList();

                    trie = CacheManager.Instance.RetrieveTrie();

                    if (trie == null)
                    {
                        trie = Utils.CreateTrieAndSaveInCache(allCitiesStrings);
                    }

                    return SendResultsList(searchString, trie, allCitiesStrings);
                }
                else
                {
                    throw new Exception(Strings.NO_SEARCH_PERFORMED);
                }
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<CitiesModel> SendResultsList(string prefix, Trie trie, List<string> allCitiesStrings)
        {
            try
            {
                List<CitiesModel> result = new List<CitiesModel>();

                if (trie == null)
                {
                    //return null;
                    throw new Exception(Strings.NULL_TRIE);
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
                    city = new CitiesModel(Strings.NO_CITY_FOUND);
                    city.Id = 0;
                    result.Add(city);
                }
                return result;
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}