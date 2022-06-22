using Microsoft.AspNetCore.Mvc;
using SailPoint_AutoComplete_ZG.Data;
using SailPoint_AutoComplete_ZG.Logic.Models;


namespace SailPoint_AutoComplete_ZG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {

        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger;            
        }

        [HttpGet]
        public List<string> Get()
        {
            return CacheManager.Instance.GetAllCitiesStringList();

        }
    }
}