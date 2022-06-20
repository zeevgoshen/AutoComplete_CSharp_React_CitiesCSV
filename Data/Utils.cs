using SailPoint_AutoComplete_ZG.Logic.Models;

namespace SailPoint_AutoComplete_ZG.Data
{
    public static class Utils
    {
        public static List<CitiesModel> ReadCSVFile()
        {
            string[] paths = { @Environment.CurrentDirectory, @"Data\world-cities_csv.csv" };
            string fullPath = Path.Combine(paths);

            StreamReader reader = new StreamReader(System.IO.File.OpenRead(fullPath));

            var citiesList = new List<CitiesModel>();
            string row;

            CitiesModel city;

            while ((row = reader.ReadLine()) != null)
            {
                city = new CitiesModel(row);

                citiesList.Add(city);

            }
            reader.Close();



            return citiesList;
        }

    }
}
