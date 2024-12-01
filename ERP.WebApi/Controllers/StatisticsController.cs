using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Core;

namespace ERP.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsServices _statisticsServices;
        public StatisticsController(IStatisticsServices statisticsServices)
        {
            _statisticsServices = statisticsServices;
        }
        [HttpGet]
        public IActionResult GetProductQuantityPerCategory()
        {
            return Ok(_statisticsServices.GetProductQuantityPerCategory());
        }
    }
}
