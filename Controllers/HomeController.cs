using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SaintBarnabasHouse.Models;

namespace SaintBarnabasHouse.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;


    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;

    }

    public IActionResult Index()
    {
        if(HttpContext.Session.GetInt32("CartId")!=null)
        {
            int cartId = (int)HttpContext.Session.GetInt32("CartId");
            int cartCount = _context.CartProductAssocs.Where(c => c.CartId == cartId).Sum(c => c.Qty);
            HttpContext.Session.SetInt32("CartCount",cartCount);
        }
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
