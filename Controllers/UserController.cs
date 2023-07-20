using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaintBarnabasHouse.Models;

namespace SaintBarnabasHouse.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private MyContext _context;


    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //Render Login/Reg page
    [HttpGet("user/login")]
    public IActionResult LoginReg()
    {
        return View("LoginReg");
    }

    //Post route for registration (redirect to success page)
    [HttpPost("register")]
    public IActionResult Register(User newUser)
    {
        if (!ModelState.IsValid)
        {
            return LoginReg();
        }

        PasswordHasher<User> hashedpwd = new PasswordHasher<User>();
        newUser.Password = hashedpwd.HashPassword(newUser, newUser.Password);
        //add to db
        _context.Users.Add(newUser);
        _context.SaveChanges();

        //log in (add to session)
        HttpContext.Session.SetInt32("UUID", newUser.UserId);
        return RedirectToAction("Index", "Home");
    }

    //Post route for LogIn (redirect to success page)
    [HttpPost("login")]
    public IActionResult Login(LoginUser loginUser)
    {
        User user = _context.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);
        if (user.IsAdmin)
        {
            HttpContext.Session.SetString("Admin", "Admin");
        }
        if (!ModelState.IsValid)
        {
            return LoginReg();
        }

        User? dbUser = _context.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);

        if (dbUser == null)
        {
            ModelState.AddModelError("Email", "not a valid email/password combo");
            return LoginReg();
        }

        PasswordHasher<LoginUser> hashedpwd = new PasswordHasher<LoginUser>();
        PasswordVerificationResult pwCompareResult = hashedpwd.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

        if (pwCompareResult == 0)
        {
            ModelState.AddModelError("Password", "not a valid email/password combo");
            return LoginReg();
        }

        HttpContext.Session.SetInt32("UUID", dbUser.UserId);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("admin/dashboard")]
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("Admin") == "Admin")
        {
            ViewBag.AllImages = _context.Images.ToList();
            ViewBag.AllCats = _context.Categories.OrderBy(c => c.Name).ToList();
            ViewBag.AllProducts = _context.Products.Include(p => p.ProductCategoryAssocs).ThenInclude(p => p.Category).Include(p => p.ProductImageAssocs).ThenInclude(p => p.Image).ToList();
            ViewBag.AllOrders = _context.Orders.Include(o => o.OrderProductAssocs).ThenInclude(opa => opa.Product).ToList();
            return View("Dashboard");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("products/{productId}/delete")]
    public IActionResult RemoveProduct(int productId)
    {
            Product prod = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (prod != null)
            {
                _context.Products.Remove(prod);
            }
            _context.SaveChanges();
        
            return Redirect(Request.Headers["Referer"].ToString());
    }
}