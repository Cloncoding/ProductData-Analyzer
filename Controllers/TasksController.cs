using Microsoft.AspNetCore.Mvc;

namespace ProductData_Analyzer.Controllers
{
    [Route("tasks")]
    [Route("/")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public ContentResult Tasks()
        {
            string html = """
                <h>Click links to API calls with specific URLs</h>

                <p><a href="https://localhost:7028/analyzer?filter=1&url=https://flapotest.blob.core.windows.net/test/ProductData.json" target="_blank"> Task 1: Most expensive and cheapest beer per litre</a>

                <p><a href="https://localhost:7028/analyzer?filter=2&url=https://flapotest.blob.core.windows.net/test/ProductData.json&price=17.99" target="_blank"> Task 2: Which beers cost exactly &euro;17.99 - Order the result by price per litre ASC</a>

                <p><a href="https://localhost:7028/analyzer?filter=3&url=https://flapotest.blob.core.windows.net/test/ProductData.json" target="_blank"> Task 3: Which one product comes in the most bottles</a>

                <p><a href="https://localhost:7028/analyzer?filter=-1&url=https://flapotest.blob.core.windows.net/test/ProductData.json&price=17.99" target="_blank"> Task All above: All answers to tasks</a>
                """;

            return new ContentResult
            {
                Content = html,
                ContentType = "text/html"
            };
        }
    }
}
