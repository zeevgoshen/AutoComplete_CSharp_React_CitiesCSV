using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Threading.Tasks;

namespace SailPoint_AutoComplete_ZG.Logic.Interfaces
{
    public interface ICitiesService
    {
        Task<IEnumerable<CitiesModel>> GetAllCities();
    }
}
