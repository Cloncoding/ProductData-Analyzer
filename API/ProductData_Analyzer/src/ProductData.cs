namespace ProductData_Analyzer.src
{
    public class ProductData
    {
        public int id { get; set; }
        public string? brandName { get; set; }
        public string? name { get; set; }
        public string? descriptionText { get; set; }
        public Article[]? articles { get; set; }


        public ProductData() { }


        public ProductData(int id, string brandName, string name, string descriptionText, Article article)
        {
            this.id = id;
            this.brandName = brandName;
            this.name = name;
            this.descriptionText = descriptionText;
            articles = [article];
        }

        public ProductData(ProductData data, Article article)
        {
            this.id = data.id;
            this.brandName = data.brandName;
            this.name = data.name;
            this.descriptionText = data.descriptionText;
            articles = [article];
        }


        public class Article
        {
            public int id { get; set; }
            public string? shortDescription { get; set; }
            public float price { get; set; }
            public string? unit { get; set; }
            public string? pricePerUnitText { get; set; }
            public string? image { get; set; }


            public Article(int id, string shortDescription, float price, string unit, string pricePerUnitText, string image)
            {
                this.id = id;
                this.shortDescription = shortDescription;
                this.price = price;
                this.unit = unit;
                this.pricePerUnitText = pricePerUnitText;
                this.image = image;
            }
        }
    }
}
