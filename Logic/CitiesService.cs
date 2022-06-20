using SailPoint_AutoComplete_ZG.Logic.Interfaces;
using SailPoint_AutoComplete_ZG.Logic.Models;

namespace SailPoint_AutoComplete_ZG.Logic
{
    public class CitiesService : ICitiesService
    {
        public async Task<IEnumerable<CitiesModel>> GetAllCities()
        {
            var allCitiesList = new List<CitiesModel>();

            StreamReader reader = new StreamReader(File.OpenRead(@"..Data\world-cities_csv.csv"));
            //string fileName = $"{oHE.WebRootPath}\\UploadedFiles\\{file.FileName}";
            

    //        using (var fileStream = file.OpenReadStream())
    //        using (var reader = new StreamReader(fileStream))
    //        {
    //            string row;
    //            while ((row = reader.ReadLine()) != null)
    //            {
    //                //... Process the row here ...
    //}
    //        }

            return allCitiesList.ToList();

        }
    }
}
