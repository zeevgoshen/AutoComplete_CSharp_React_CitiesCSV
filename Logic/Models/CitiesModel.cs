namespace SailPoint_AutoComplete_ZG.Logic.Models
{
    public class CitiesModel
    {
        public string CityName { get; set; } = String.Empty;
        public CitiesModel(string name)
        {
            CityName = name;
        }
    }
}
