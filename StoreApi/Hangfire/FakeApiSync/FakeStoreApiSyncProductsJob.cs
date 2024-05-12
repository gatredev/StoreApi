using StoreApi.Database;
using StoreApi.Database.Entity;
using System.Text.Json.Serialization;

namespace StoreApi.Hangfire.FakeApiSync;

public class FakeStoreApiSyncProductsJob
{
    private readonly HttpClient _httpClient;
    private readonly StoreDbContext _dbContext;
    private readonly string _baseURL = "https://fakestoreapi.com/";

    public FakeStoreApiSyncProductsJob(HttpClient httpClient, StoreDbContext dbContext)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
    }

    public async Task Run()
    {
        var apiProducts = await _httpClient.GetFromJsonAsync<List<ProductDTO>>($"{_baseURL}products");
        if (apiProducts?.Any() is not true)
            return;

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

        var categories = apiProducts.Select(x => x.Category)
            .Distinct()
            .Where(i => !string.IsNullOrWhiteSpace(i))
            .Select(i => new Category { Name = i! })
            .ToList();

        var mappedDbProducts = apiProducts
            .Select(apiProduct => MapFromApi(apiProduct, categories))
            .ToList();
        _dbContext.Products.AddRange(mappedDbProducts);
        _dbContext.SaveChanges();
    }

    private Product MapFromApi(ProductDTO apiProducts, List<Category> categories) =>
        new Product
        {
            FakeStoreId = apiProducts.FakeStoreId,
            Title = apiProducts.Title,
            Description = apiProducts.Description,
            Category = categories.FirstOrDefault(i => i.Name == apiProducts.Category),
            Image = apiProducts.Image,
            Price = apiProducts.Price,
            Rate = apiProducts.Rating.Rate,
            RatingCount = apiProducts.Rating.Count,
        };
}

public record ProductDTO
{
    [JsonPropertyName("id")]
    public int FakeStoreId { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("rating")]
    public Rating? Rating { get; set; }
}

public record Rating
{
    [JsonPropertyName("rate")]
    public decimal Rate { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}

