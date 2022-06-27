using SailPoint_AutoComplete_ZG.Constants;
using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using TriesLib;

namespace SailPoint_AutoComplete_ZG.Data
{
    public static class Utils
    {
        /// <summary>
        /// Server side validations
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidSearchText(string text)
        {   
            Regex regex = new Regex(@"^[a-z -']");
            return regex.Match(text).Success;
        }
        
        /// <summary>
        /// Reads the csv file
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async static Task<List<string>> ReadCSVFileToStringList()
        {
            string[] paths = { @Environment.CurrentDirectory, Strings.DATA_FILE_WIN };
            string fullPath = Path.Combine(paths);
            StreamReader reader = null;

            try
            {

                if (!File.Exists(fullPath))
                {
                    string[] osxPaths = { @Environment.CurrentDirectory, Strings.DATA_FILE_OSX };
                    fullPath = Path.Combine(osxPaths);
                }

                reader = new StreamReader(File.OpenRead(fullPath));

                var citiesList = new List<string>();
                string row;

                await Task.Run(() => { 
                    while ((row = reader.ReadLine()) != null)
                    {
                        citiesList.Add(row);
                    }
                    reader.Close();
                });

                return citiesList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Creates a pre-fix trie and stores it in cache
        /// </summary>
        /// <param name="allCitiesStrings"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Trie CreateTrieAndSaveInCache(List<string> allCitiesStrings)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
