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

    [HttpGet("blog")]
    public IActionResult Blog()
    {
        return View("Blog");
    }

    [HttpGet("blogpost")]
    public IActionResult BlogPost()
    {
        return View("BlogPost");
    }

    [HttpGet("event")]
    public IActionResult Event()
    {
        return View("Event");
    }
}
