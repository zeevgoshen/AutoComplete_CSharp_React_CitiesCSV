using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;

namespace SailPoint_AutoComplete_ZG.Data
{
    public static class Utils
    {
        public static ConcurrentBag<CitiesModel> ReadCSVFile()
        {
            string[] paths = { @Environment.CurrentDirectory, @"Data\world-cities_csv.csv" };
            string fullPath = Path.Combine(paths);

            StreamReader reader;

            if (!File.Exists(fullPath))
            {                
                string[] osxPaths = { @Environment.CurrentDirectory, @"Data/world-cities_csv.csv" };
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
            string[] paths = { @Environment.CurrentDirectory, @"Data\world-cities_csv.csv" };
            string fullPath = Path.Combine(paths);

            StreamReader reader;

            if (!File.Exists(fullPath))
            {
                string[] osxPaths = { @Environment.CurrentDirectory, @"Data/world-cities_csv.csv" };
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
    }
}
