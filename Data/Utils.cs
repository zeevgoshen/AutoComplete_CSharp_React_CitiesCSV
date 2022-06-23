using SailPoint_AutoComplete_ZG.Constants;
using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;
using TriesLib;

namespace SailPoint_AutoComplete_ZG.Data
{
    public static class Utils
    {
        public static ConcurrentBag<CitiesModel> ReadCSVFile()
        {
            string[] paths = { @Environment.CurrentDirectory, Strings.DATA_FILE_WIN };
            string fullPath = Path.Combine(paths);

            StreamReader reader;

            if (!File.Exists(fullPath))
            {                
                string[] osxPaths = { @Environment.CurrentDirectory, Strings.DATA_FILE_OSX };
                fullPath = Path.Combine(osxPaths);
            }

            reader = new StreamReader(File.OpenRead(fullPath));
            var citiesList = new ConcurrentBag<CitiesModel>();
            string row;

            CitiesModel city;
            int i = 0;

            while ((row = reader.ReadLine()) != null)
            {
                city = new CitiesModel(row);
                city.Id = i++;
                citiesList.Add(city);
            }
            reader.Close();

            return citiesList;
        }


        public static List<string> ReadCSVFileToStringList()
        {
            string[] paths = { @Environment.CurrentDirectory, Strings.DATA_FILE_WIN };
            string fullPath = Path.Combine(paths);

            StreamReader reader;

            if (!File.Exists(fullPath))
            {
                string[] osxPaths = { @Environment.CurrentDirectory, Strings.DATA_FILE_OSX };
                fullPath = Path.Combine(osxPaths);
            }

            reader = new StreamReader(File.OpenRead(fullPath));

            var citiesList = new List<string>();
            string row;

            while ((row = reader.ReadLine()) != null)
            {
                citiesList.Add(row);
            }
            reader.Close();

            return citiesList;
        }

        public static Trie CreateTrieAndSaveInCache(List<string> allCitiesStrings)
        {
            Trie trie = new Trie();

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
            return trie;
        }
    }
}
