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
        ObjectCache? cache = null;
        CacheItemPolicy? policy = null;

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

        // This is used when the "home page" first loads.
        // before any searches are done.
        public async Task<List<string>> GetAllCitiesStringList()
        {
            try
            {
                List<string>? allCities = new List<string>();

                if (cache != null)
                {
                    allCities = cache[Strings.CACHE_KEY_CITIES] as List<string>;
                }

                if (allCities == null)
                {
                    allCities = await Task.Run(() => Utils.ReadCSVFileToStringList());

                    if (cache != null)
                    {
                        cache.Set(Strings.CACHE_KEY_CITIES, allCities, policy);
                    }
                }

                return allCities;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveTrie(Trie trie)
        {
            try
            {
                if (cache != null)
                {
                    cache.Set(Strings.CACHE_KEY_TRIE, trie, policy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Trie? RetrieveTrie()
        {
            try
            {
                if (cache != null)
                {
                    return cache["trie"] as Trie;
                }
                else
                {
                    return new Trie();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
