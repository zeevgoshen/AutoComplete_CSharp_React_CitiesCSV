using Microsoft.AspNetCore.Mvc;
using SailPoint_AutoComplete_ZG.Data;
using SailPoint_AutoComplete_ZG.Logic.Models;
using System.Collections.Concurrent;

namespace SailPoint_AutoComplete_ZG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : Controller
    {

        private readonly ILogger<CitiesController> _logger;

        // the logger is not used but is still here for future use
        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async static Task<List<string>> Get()
        {
            try
            {
                return await CacheManager.Instance.GetAllCitiesStringList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}