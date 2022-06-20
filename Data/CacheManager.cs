using SailPoint_AutoComplete_ZG.Logic.Models;

namespace SailPoint_AutoComplete_ZG.Data
{
    public sealed class CacheManager
    {
        private static object syncRoot = new Object();
        private static CacheManager instance;

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

        public List<CitiesModel> GetAllCities()
        {
            List<CitiesModel>? allCities = System.Runtime.Caching.MemoryCache.Default["names"] as List<CitiesModel>;
            if (allCities == null)
            {
                allCities = Utils.ReadCSVFile();
                System.Runtime.Caching.MemoryCache.Default["names"] = allCities;

            }

            return allCities;
        }
    }
}
