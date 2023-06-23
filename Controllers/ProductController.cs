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

            newProduct.PriceAsInt = (int)(newProduct.Price * 100);
            _context.Add(newProduct);
            _context.SaveChanges();
            return AddCategory(newProduct.ProductId);
        }
        else
        {
            return View("NewProduct");
        }
    }

    // Render the edit page
    [HttpGet("products/{ProductId}/edit")]
    public IActionResult EditProduct(int ProductId)
    {
        Product? ProductToEdit = _context.Products.FirstOrDefault(i => i.ProductId == ProductId);

        if (ProductToEdit == null)
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }

        return View("UpdateProduct", ProductToEdit);
    }

    // Handle updating and redirect to Show page
    [HttpPost("products/{ProductId}/update")]
    public IActionResult ProductUpdate(Product newProduct, int ProductId)
    {
        if (!ModelState.IsValid)
        {
            return EditProduct(ProductId);
        }

        Product? OldProduct = _context.Products.FirstOrDefault(i => i.ProductId == ProductId);

        if (OldProduct == null)
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        newProduct.PriceAsInt = (int)(newProduct.Price * 100);
        OldProduct.Name = newProduct.Name;
        OldProduct.Description = newProduct.Description;
        OldProduct.Price = newProduct.Price;
        OldProduct.MainCat = newProduct.MainCat;
        OldProduct.PriceAsInt = newProduct.PriceAsInt;
        OldProduct.UpdatedAt = DateTime.Now;

        _context.SaveChanges();

        return Redirect(Request.Headers["Referer"].ToString());

    }


    //Render add category to product form
    [HttpGet("products/{productId}/addcat")]
    public IActionResult AddCategory(int productId)
    {
        Product? OneProduct = _context.Products.Include(p => p.ProductCategoryAssocs).ThenInclude(a => a.Category).FirstOrDefault(p => p.ProductId == productId);
        if (OneProduct == null)
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }

        ViewBag.AllCategories = _context.Categories.OrderBy(c => c.Name).ToList();

        return View("AddCategory", OneProduct);
    }

    //Add the category to the product, then reload the page
    [HttpPost("products/{productId}/addcat")]
    public IActionResult AddCatToProd(int[] CatIds, int productId)
    {
        foreach (int cat in CatIds)
        {
            if (!_context.ProductCategoryAssocs.Any(pca => pca.CategoryId == cat && pca.ProductId == productId))
            {
                ProductCategoryAssoc newAssoc = new() { CategoryId = cat, ProductId = productId };
                _context.Add(newAssoc);
            }
        }
        _context.SaveChanges();
        return AddCategory(productId);
    }

    [HttpPost("products/{productId}/removecat")]
    public IActionResult RemoveCatFromProd(int[] CatIds, int productId)
    {
        foreach (int cat in CatIds)
        {
            ProductCategoryAssoc assoc = _context.ProductCategoryAssocs.FirstOrDefault(pca => pca.CategoryId == cat && pca.ProductId == productId);
            if (assoc != null)
            {
                _context.ProductCategoryAssocs.Remove(assoc);
            }
            _context.SaveChanges();
        }
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

    //Render add Image to product form
    [HttpGet("products/{productId}/addimg")]
    public IActionResult AddImage(int productId)
    {
        Product? OneProduct = _context.Products.Include(p => p.ProductImageAssocs).ThenInclude(a => a.Image).FirstOrDefault(p => p.ProductId == productId);
        if (OneProduct == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.AllImages = _context.Images.OrderByDescending(i => i.ImageId).ToList();

        return View("AddImage", OneProduct);
    }

    //Add the Image to the product, then reload the page
    //Uses checkboxes to populate a list
    [HttpPost("products/{productId}/addimg")]
    public IActionResult AddImgToProd(int[] ImageIds, int productId)
    {
        foreach (int img in ImageIds)
        {
            if (!_context.ProductImageAssocs.Any(pia => pia.ImageId == img && pia.ProductId == productId))
            {
                ProductImageAssoc newAssoc = new() { ImageId = img, ProductId = productId };
                _context.Add(newAssoc);
            }
        }
        _context.SaveChanges();
        return AddImage(productId);
    }

    [HttpPost("Images/new")]
    public IActionResult CreateImage(Image newImage)
    {
        if (!_context.Images.Any(img => img.ImageUrl == newImage.ImageUrl))
        {
            _context.Add(newImage);
            _context.SaveChanges();
        }
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpPost("products/{productId}/removeimg")]
    public IActionResult RemoveImgFromProd(int[] ImgIds, int productId)
    {
        foreach (int img in ImgIds)
        {
            ProductImageAssoc assoc = _context.ProductImageAssocs.FirstOrDefault(pca => pca.ImageId == img && pca.ProductId == productId);
            if (assoc != null)
            {
                _context.ProductImageAssocs.Remove(assoc);
            }
            _context.SaveChanges();
        }
        return AddImage(productId);
    }

    [HttpPost("products/{productId}/setmainimage")]
    public IActionResult SetMainImage(string url, int productId)
    {
        Console.WriteLine("Hit the route");
        Product prod = _context.Products.FirstOrDefault(p => p.ProductId == productId);
        prod.MainImgUrl = url;
        prod.UpdatedAt = DateTime.Now;

        _context.SaveChanges();

        return Redirect(Request.Headers["Referer"].ToString());
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