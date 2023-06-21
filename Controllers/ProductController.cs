//This controller handles navigation within the shop and Product CRUD

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaintBarnabasHouse.Models;

namespace SaintBarnabasHouse.Controllers;

//TODO: Create an Admin verification to be applied to Create, Update, and Delete routes
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;

    private MyContext _context;


    public ProductController(ILogger<ProductController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;

    }

    //Render Create New product page
    [HttpGet("products/new")]
    public IActionResult RenderNewProduct()
    {
        return View("NewProduct");
    }

    // Handle create form
    [HttpPost("products/new")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if (ModelState.IsValid)
        {   
            
            newProduct.PriceAsInt=(int)(newProduct.Price*100);
            _context.Add(newProduct);
            _context.SaveChanges();
            return AddCategory(newProduct.ProductId);
        }
        else
        {
            return View("NewProduct");
        }
    }

    //Render add category to product form
    [HttpGet("products/{productId}")]
    public IActionResult AddCategory(int productId)
    {
        Product? OneProduct = _context.Products.Include(p => p.ProductCategoryAssocs).ThenInclude(a => a.Category).FirstOrDefault(p => p.ProductId == productId);
        if (OneProduct == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.AllCategories = _context.Categories.OrderBy(c => c.Name).ToList();

        return View("AddCategory", OneProduct);
    }

    //Add the category to the product, then reload the page
    [HttpPost("products/{productId}/addcat")]
    public IActionResult AddCatToProd(ProductCategoryAssoc newAssoc, int productId)
    {
        _context.Add(newAssoc);
        _context.SaveChanges();
        return AddCategory(productId);
    }

    //Render Create New Category page (adds categories to list of categories)
    [HttpGet("categories/new")]
    public IActionResult RenderNewCategory()
    {
        ViewBag.AllCats = _context.Categories.ToList();
        return View("NewCategory");
    }

    [HttpPost("categories/new")]
    public IActionResult CreateCategory(Category newCategory)
    {
        if (!_context.Categories.Any(cat => cat.Name == newCategory.Name))
        {
            _context.Add(newCategory);
            _context.SaveChanges();
        }
        return RedirectToAction("RenderNewCategory");
    }
}

// //Render Create New dish page
//     [HttpGet("dishes/new")]
//     public IActionResult RenderNewDish()
//     {
//         return View("Create");
//     }

// // Handle create form
//     [HttpPost("create")]
//     public IActionResult CreateDish(Dish newDish)
//     {
//         if(ModelState.IsValid)
//         {
//             _context.Add(newDish);
//             _context.SaveChanges();
//             return RedirectToAction("Index");
//         }
//         else 
//         {
//         return View("Create");
//         }
//     }

// // Read one (Show one dish)
//     [HttpGet("dishes/{id}")]
//     public IActionResult ShowDish(int id)
//     {
//         Dish? OneDish = _context.Dishes.FirstOrDefault(i=>i.DishId == id);
//         if(OneDish == null)
//         {
//             return RedirectToAction("Index");
//         }

//         return View("OneDish",OneDish);
//     }


// // Render the edit page
//     [HttpGet("dishes/{DishId}/edit")]
//     public IActionResult EditDish(int DishId)
//     {
//         Dish? DishToEdit = _context.Dishes.FirstOrDefault(i => i.DishId == DishId);

//         if(DishToEdit == null)
//         {
//             return RedirectToAction("Index");
//         }

//         return View("Edit",DishToEdit);
//     }

// // Handle updating and redirect to Show page
//     [HttpPost("dishes/{DishId}/update")]
//     public IActionResult UpdateDish(Dish newDish, int DishId)
//     {
//         if(!ModelState.IsValid)
//         {
//             return EditDish(DishId);
//         }

//         Dish? OldDish = _context.Dishes.FirstOrDefault(i => i.DishId == DishId);

//         if(OldDish == null)
//         {
//             return RedirectToAction("Index");
//         }
//             OldDish.Name = newDish.Name;
//             OldDish.Chef = newDish.Chef;
//             OldDish.Tastiness = newDish.Tastiness;
//             OldDish.Calories = newDish.Calories;
//             OldDish.Description = newDish.Description;
//             OldDish.UpdatedAt = DateTime.Now;

//             _context.SaveChanges();

//             return RedirectToAction("ShowDish", new { id = OldDish.DishId });

//     }


// //Delete dish and redirect to Index
//     [HttpPost("dishes/{DishId}/delete")]
//     public IActionResult DeleteDish(int DishId)
//     {
//         Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);

//         if(dish != null)
//         {
//             _context.Dishes.Remove(dish);
//             _context.SaveChanges();
//         }
//         return RedirectToAction("Index");
//     }