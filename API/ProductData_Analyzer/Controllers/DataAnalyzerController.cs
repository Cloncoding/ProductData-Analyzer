using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductData_Analyzer.src;
using System.Text.Json;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.Extensions.Options;

namespace ProductData_Analyzer.Controllers
{
    [ApiController]
    [Route("analyzer")]
    public class DataAnalyzerController : ControllerBase
    {
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };


        [HttpGet]
        public async Task<string> GetAsync(string? url, float? price, int? filter = -1)
        {
            if(url == null)
            {
                return "Please provide URL as query paremeter!";
            }

            List<ProductData> data = await GetFromUrl(url);


            switch(filter)
            {
                case 1:
                    {
                        return GetMostExpensiveAndCheapest(data);
                    }
                case 2:
                    {
                        if(price == null)
                        {
                            return "Please provide price as query paremeter!";
                        }

                        return GetWithSpecificPrice(data, price);
                    }
                case 3:
                    {
                        return GetMostBottles(data);
                    }
                default:
                    {
                        if(price == null)
                        {
                            return "Please provide price as query paremeter!";
                        }

                        return GetAll(data, price);
                    }
            }

            return filter + " " + url + " " + price;
        }

        private async Task<List<ProductData>> GetFromUrl(string url)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(url)
            };

            HttpResponseMessage response = await client.GetAsync("");
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ProductData>>(jsonResponse);
        }


        private string GetMostExpensiveAndCheapest(List<ProductData> data)
        {
            ProductData? mostExpensive = null, cheapest = null;
            float mostExpensivePrice = 0, cheapestPrice = 0;

            foreach(var product in data)
            {
                if(product.articles ==  null || product.articles.Length < 1)
                {
                    continue;
                }

                foreach(var article in product.articles)
                {
                    if(article.pricePerUnitText == null)
                    {
                        continue;
                    }

                    float ppl = ExtractPricePerLiter(article.pricePerUnitText);

                    if(mostExpensive == null || ppl > mostExpensivePrice)
                    {
                        mostExpensivePrice = ppl;
                        mostExpensive = new ProductData(product, article);
                    }
                    else if(cheapest == null || ppl < cheapestPrice)
                    {
                        cheapestPrice = ppl;
                        cheapest = new ProductData(product, article);
                    }
                }
            }

            return JsonSerializer.Serialize(new ProductData[] { mostExpensive, cheapest }, options);
        }

        private float ExtractPricePerLiter(string text)
        {
            var match = Regex.Match(text.Replace(',', '.'), @"([-+]?[0-9]*\.?[0-9]+)");
            return Convert.ToSingle(match.Groups[1].Value, CultureInfo.InvariantCulture);
        }

        private string GetWithSpecificPrice(List<ProductData> data, float? price)
        {
            List<ProductData> match = new List<ProductData>();

            foreach(var product in data)
            {
                if(product.articles == null || product.articles.Length < 1)
                {
                    continue;
                }

                foreach(var article in product.articles)
                {
                    if(article.pricePerUnitText == null)
                    {
                        continue;
                    }

                    if(article.price.ToString().Equals(price.ToString()))
                    {
                        match.Add(new ProductData(product, article));
                    }
                }
            }

            return JsonSerializer.Serialize(match.OrderBy(p => ExtractPricePerLiter(p.articles[0].pricePerUnitText)).ToArray(), options);
        }

        private string GetMostBottles(List<ProductData> data)
        {
            ProductData? mostBottlesProduct = null;
            int mostBottles = 0;

            foreach(var product in data)
            {
                if(product != null && product.articles != null && product.articles.Length > 0)
                {
                    foreach(var article in product.articles)
                    {
                        if(article.shortDescription == null)
                        {
                            continue;
                        }

                        int bottles = Convert.ToInt32(article.shortDescription.Split('x')[0].Trim());

                        if(bottles > mostBottles)
                        {
                            mostBottles = bottles;
                            mostBottlesProduct = new ProductData(product, article);
                        }
                    }
                }
            }

            return JsonSerializer.Serialize(mostBottlesProduct, options);
        }

        private string GetAll(List<ProductData> data, float? price)
        {
            return JsonSerializer.Serialize(
                new List<string>() { GetMostExpensiveAndCheapest(data), GetWithSpecificPrice(data, price), GetMostBottles(data) },
                options);
        }
    }
}
