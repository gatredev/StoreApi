namespace StoreApi.Models.Products
{
    public class SearchProductsRequestModel
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Title { get; set; }
        public string? Category { get; set; }
    }
}
