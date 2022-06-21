﻿using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;
using System.Runtime.Caching;
using TriesLib;

namespace SailPoint_AutoComplete_ZG.Data
{
    public sealed class CacheManager
    {
        private static object syncRoot = new Object();
        private static CacheManager? instance;
        ObjectCache cache = MemoryCache.Default;
        CacheItemPolicy policy = new CacheItemPolicy();

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
            ConcurrentBag<CitiesModel>? allCities = MemoryCache.Default["allCities"] as ConcurrentBag<CitiesModel>;
            
            if (allCities == null)
            {
                allCities = Utils.ReadCSVFile();
                MemoryCache.Default["allCities"] = allCities;                
            }

            return allCities;
        }

        // This is used when the "home page" first loads.
        // before any searches are done.
        public List<string> GetAllCitiesStringList()
        {
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60.0);

            List<string>? allCities = cache["allCitiesStrings"] as List<string>;

            if (allCities == null)
            {
                allCities = Utils.ReadCSVFileToStringList();
                cache.Set("allCitiesStrings",allCities, policy);
            }

            return allCities;
        }

        public void SaveTrieOfFirstLetter(Trie trie, string searchString)
        {
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60.0);

            cache.Set(searchString, trie, policy);
            //MemoryCache.Default[searchString] = trie;
        }

        public Trie RetrieveTrieOfFirstLetter(string searchString)
        {
            string key = string.Empty;

            if (searchString != null)
            {
                key = searchString.Substring(0,1);
                return cache[key] as Trie;
            }
            return null;
        }
    }
}
