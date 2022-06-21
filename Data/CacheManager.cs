using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;
using TriesLib;

namespace SailPoint_AutoComplete_ZG.Data
{
    public sealed class CacheManager
    {
        private static object syncRoot = new Object();
        private static CacheManager? instance;

        public static CacheManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
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
            ConcurrentBag<CitiesModel>? allCities = System.Runtime.Caching.MemoryCache.Default["allCities"] as ConcurrentBag<CitiesModel>;
            
            if (allCities == null)
            {
                allCities = Utils.ReadCSVFile();
                System.Runtime.Caching.MemoryCache.Default["allCities"] = allCities;                
            }

            return allCities;
        }

        public List<string> GetAllCitiesStringList()
        {
            List<string>? allCities = System.Runtime.Caching.MemoryCache.Default["allCitiesStrings"] as List<string>;

            if (allCities == null)
            {
                allCities = Utils.ReadCSVFileToStringList();
                System.Runtime.Caching.MemoryCache.Default["allCitiesStrings"] = allCities;
            }

            return allCities;
        }

        public void SaveTrieOfFirstLetter(Trie trie, string searchString)
        {
            System.Runtime.Caching.MemoryCache.Default[searchString] = trie;
        }

        public Trie RetrieveTrieOfFirstLetter(string searchString)
        {
            string key = string.Empty;

            if (searchString != null)
            {
                key = searchString.Substring(0,1);
                return System.Runtime.Caching.MemoryCache.Default[key] as Trie;
            }
            return null;
        }
    }
}
