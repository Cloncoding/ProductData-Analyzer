using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductData_Analyzer.Controllers
{
    [ApiController]
    [Route("analyzer/[controller]")]
    public class DataAnalyzerController : ControllerBase
    {

        public DataAnalyzerController()
        {
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "" };
        }
    }
}
