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
                <h> Click links to test API calls with specific URLs </h>
                <p> URLs with a filter parameter <br>
                    <a href="http://localhost:7028/analyzer?filter=1&url=https://flapotest.blob.core.windows.net/test/ProductData.json" target="_blank"> Task 1: Most expensive and cheapest beer per litre </a><br>
                    <a href="http://localhost:7028/analyzer?filter=2&url=https://flapotest.blob.core.windows.net/test/ProductData.json&price=17.99" target="_blank"> Task 2: Which beers cost exactly &euro;17.99 - Order the result by price per litre ASC </a><br>
                    <a href="http://localhost:7028/analyzer?filter=3&url=https://flapotest.blob.core.windows.net/test/ProductData.json" target="_blank"> Task 3: Which one product comes in the most bottles </a><br>
                    <a href="http://localhost:7028/analyzer?url=https://flapotest.blob.core.windows.net/test/ProductData.json&price=17.99" target="_blank"> Task All above: All answers to tasks </a><br>
                    Example: /analyzer?filter=2&url=https://flapotest.blob.core.windows.net/test/ProductData.json&price=17.99<br>
                    The filter differentiates between the given exercises [1. Task = 1, 2. Task = 2, 3. Task = 3]. If no filter is given or the number cannot be mapped to a task, all questions are answered.
                </p>
                <p> URLs with different routes <br>
                    <a href="http://localhost:7028/analyzer/mostExpensiveAndCheapest?url=https://flapotest.blob.core.windows.net/test/ProductData.json" target="_blank"> Task 1: Most expensive and cheapest beer per litre </a><br>
                    <a href="http://localhost:7028/analyzer/specificPrice?&url=https://flapotest.blob.core.windows.net/test/ProductData.json&price=17.99" target="_blank"> Task 2: Which beers cost exactly &euro;17.99 - Order the result by price per litre ASC </a><br>
                    <a href="http://localhost:7028/analyzer/mostBottles?url=https://flapotest.blob.core.windows.net/test/ProductData.json" target="_blank"> Task 3: Which one product comes in the most bottles </a><br>
                    <a href="http://localhost:7028/analyzer/all?url=https://flapotest.blob.core.windows.net/test/ProductData.json&price=17.99" target="_blank"> Task All above: All answers to tasks </a><br>
                    Example: /analyzer/specificPrice?&url=https://flapotest.blob.core.windows.net/test/ProductData.json&price=17.99<br>
                    All Tasks have different routes [1. Task = mostExpensiveAndCheapest, 2. Task = specificPrice, 3. Task = mostBottles, All Tasks = all].
                </p>
                """;

            return new ContentResult
            {
                Content = html,
                ContentType = "text/html"
            };
        }
    }
}
