namespace StoreApi.Controllers;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Database;
using StoreApi.Models.Products;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly StoreDbContext _storeContext;

    public ProductsController(StoreDbContext storeContext)
    {
        _storeContext = storeContext;
    }

    [HttpGet]
    [ProducesResponseType<List<SearchProductsResponseModel>>(StatusCodes.Status200OK)]
    public IActionResult SearchProducts([FromQuery] SearchProductsRequestModel request)
    {
        var productsQuery = _storeContext.Products.Include(i => i.Category).AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            productsQuery = productsQuery.Where(i => i.Title!.Contains(request.Title, StringComparison.InvariantCultureIgnoreCase));
        }
        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            productsQuery = productsQuery.Where(i => i.Category!.Name.Contains(request.Category, StringComparison.InvariantCultureIgnoreCase));
        }

        productsQuery = productsQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);

        var products = productsQuery
            .Select(i => new SearchProductsResponseModel
            {
                Category = i.Category!.Name,
                Description = i.Description,
                Image = i.Image,
                Price = i.Price,
                Rate = i.Rate,
                RatingCount = i.RatingCount,
                Title = i.Title,
            });

        return Ok(products.ToList());
    }
}
