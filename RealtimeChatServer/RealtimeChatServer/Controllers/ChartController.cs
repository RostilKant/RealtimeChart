using Microsoft.AspNetCore.Mvc;
using Services;

namespace RealtimeChatServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IChartService _chartService;
        public ChartController(IChartService chartService)
        {
            _chartService = chartService;
        }
        public IActionResult Get()
        {
            _chartService.PostDataToClient();
            return Ok(new {Message = "Request completed"});
        }
    }
}