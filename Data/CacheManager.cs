using SailPoint_AutoComplete_ZG.Constants;
using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;
using System.Runtime.Caching;
using TriesLib;

namespace SailPoint_AutoComplete_ZG.Data
{
    public sealed class CacheManager
    {
        private static object syncRoot = new Object();
        private static CacheManager? instance;
        ObjectCache cache = null;
        CacheItemPolicy policy = null;

        private CacheManager()
        {
            cache = MemoryCache.Default;
            policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60.0);
        }

        // Double-check Singleton for thread safety.
        public static CacheManager Instance
        {
            get
            {
                // #1
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        // #2
                        if (instance == null)
                        {
                            instance = new CacheManager();
                        }
                    }
                }
                return instance;
            }
        }

        public ConcurrentBag<CitiesModel> GetAllCities()
        {
            ConcurrentBag<CitiesModel>? allCities = MemoryCache.Default[Strings.CACHE_KEY_CITIES_CONC] as ConcurrentBag<CitiesModel>;

            if (allCities == null)
            {
                allCities = Utils.ReadCSVFile();
                MemoryCache.Default[Strings.CACHE_KEY_CITIES_CONC] = allCities;
            }

            return allCities;
        }

        // This is used when the "home page" first loads.
        // before any searches are done.
        public List<string> GetAllCitiesStringList()
        {
            List<string>? allCities = cache[Strings.CACHE_KEY_CITIES] as List<string>;

            if (allCities == null)
            {
                allCities = Utils.ReadCSVFileToStringList();
                cache.Set(Strings.CACHE_KEY_CITIES, allCities, policy);
            }

            return allCities;
        }

        public void SaveTrie(Trie trie)
        {
            try
            {
                cache.Set(Strings.CACHE_KEY_TRIE, trie, policy);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Trie RetrieveTrie()
        {
            try
            {
                return cache["trie"] as Trie;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
