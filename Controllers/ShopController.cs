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

    [HttpGet("shop")]
    public IActionResult Shop()
    {
        List<Product> AllProducts = _context.Products.Include(p => p.ProductCategoryAssocs).ThenInclude(p => p.Category).ToList();
        return View("Shop",AllProducts);
    }
}