using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaintBarnabasHouse.Models;

namespace SaintBarnabasHouse.Controllers;

public class ShopController : Controller
{
    private readonly ILogger<ShopController> _logger;

    private MyContext _context;


    public ShopController(ILogger<ShopController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;

    }
    //Render main shop page
    [HttpGet("shop")]
    public IActionResult Shop()
    {
        List<Product> AllProducts = _context.Products.Include(p => p.ProductCategoryAssocs).ThenInclude(p => p.Category).Include(p => p.ProductImageAssocs).ThenInclude( p => p.Image).ToList();
        return View("Shop", AllProducts);
    }

    //Render shop page filtered by main category (books, crafts, food)
    [HttpGet("shop/category/{mainCat}")]
    public IActionResult FilterCat(string mainCat)
    {
        List<Product> FilteredProducts = _context.Products.Where(p => p.MainCat == mainCat).Include(p => p.ProductCategoryAssocs).ThenInclude(p => p.Category).ToList();
        ViewBag.Heading = mainCat;
        ViewBag.Cats = _context.Categories.ToList();
        return View("FilteredShop", FilteredProducts);
    }

    //POST for search function
    [HttpPost("shop/searchresults")]
    public IActionResult Search(string query)
    {
        //Add a line to remove whatever to prevent SQL injection
        string[] keywords = query.Split(" ");
        List<Product> Results = new();
        foreach (string keyword in keywords)
        {
            List<Product> QueryResult = (_context.Products.Where(p => p.Name.Contains(keyword) || p.Description.Contains(query)).Include(p => p.ProductCategoryAssocs).ThenInclude(p => p.Category).ToList());
            foreach (Product item in QueryResult)
            {
                if(!Results.Contains(item)){
                Results.Add(item);
                }
            }
        }
        ViewBag.Heading = "Search Results";
        ViewBag.Categories = _context.Categories.ToList();
        return View("FilteredShop", Results);
    }

    [HttpGet("shop/products/{productId}")]
    public IActionResult ShowProduct(int productId)
    {
        Product? OneProduct = _context.Products.Include(p => p.ProductCategoryAssocs).ThenInclude(p => p.Category).Include(p => p.ProductImageAssocs).ThenInclude(p => p.Image).FirstOrDefault(p => p.ProductId == productId);
        if (OneProduct == null)
        {
            return RedirectToAction("Shop");
        }

        return View("ShowItem", OneProduct);
    }
}