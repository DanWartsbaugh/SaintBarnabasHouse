using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SaintBarnabasHouse.Models;

namespace SaintBarnabasHouse.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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

        [HttpGet("shop")]
    public IActionResult Shop()
    {
        return View("Shop");
    }

        [HttpGet("event")]
    public IActionResult Event()
    {
        return View("Event");
    }
}
