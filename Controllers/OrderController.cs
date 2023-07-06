using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaintBarnabasHouse.Models;

namespace SaintBarnabasHouse.Controllers;

public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;

    private MyContext _context;


    public OrderController(ILogger<OrderController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //Render shopping cart
    [HttpGet("shop/cart")]
    public IActionResult ShowCart()
    {
        List<CartProductAssoc> assocs = _context.CartProductAssocs.Include(cpa => cpa.Product).Where(cpa => cpa.CartId == HttpContext.Session.GetInt32("CartId")).ToList();
        return View("Cart", assocs);
    }

    //Add item to cart - create a new cart if none in session. Add CPAssoc.
    [HttpPost("shop/add/{productId}")]
    public IActionResult AddToCart(int productId, int Qty)
    {
        if (HttpContext.Session.GetInt32("CartId") == null)
        {
            Cart newCart = new Cart();
            if (HttpContext.Session.GetInt32("UUID") != null)
            {
                newCart.UserId = HttpContext.Session.GetInt32("UUID");
            }
            _context.Add(newCart);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("CartId", newCart.CartId);
        }
        CartProductAssoc newAssoc = new CartProductAssoc()
        {
            CartId = (int)HttpContext.Session.GetInt32("CartId"),
            ProductId = productId,
            Qty = Qty
        };
        _context.Add(newAssoc);
        _context.SaveChanges();

        int cartId = (int)HttpContext.Session.GetInt32("CartId");
        int cartCount = _context.CartProductAssocs.Where(c => c.CartId == cartId).Sum(c => c.Qty);
        HttpContext.Session.SetInt32("CartCount", cartCount);

        return Redirect(Request.Headers["Referer"].ToString());
    }


    //Checkout - create a new order. Put order number in Session. Create a new OPAssoc foreach CPAssoc. Redirect to Billing page.
    [HttpPost("shop/order")]
    public IActionResult Checkout()
    {
        int CartId = (int)HttpContext.Session.GetInt32("CartId");
        if (HttpContext.Session.GetInt32("OrderId") == null)
        {
            Order newOrder = new Order();
            newOrder.CartId = (int)HttpContext.Session.GetInt32("CartId");
            if(HttpContext.Session.GetInt32("UUID") != null)
            {
                newOrder.UserId = (int)HttpContext.Session.GetInt32("UUID");
            }
            _context.Add(newOrder);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("OrderId", newOrder.OrderId);

            List<CartProductAssoc> items = _context.CartProductAssocs.Where(cpa => cpa.CartId == CartId).ToList();
            foreach (CartProductAssoc item in items)
            {
                OrderProductAssoc newAssoc = new OrderProductAssoc()
                {
                    ProductId = item.ProductId,
                    OrderId = (int)HttpContext.Session.GetInt32("OrderId"),
                    Qty = item.Qty
                };
                _context.Add(newAssoc);
            }
            _context.SaveChanges();
        }

        else
        {
            List<OrderProductAssoc> order = _context.OrderProductAssocs.Where(o => o.OrderId == HttpContext.Session.GetInt32("OrderId")).ToList();
            List<CartProductAssoc> items = _context.CartProductAssocs.Where(cpa => cpa.CartId == CartId).ToList();
            foreach (CartProductAssoc item in items)
            {
                if (order.Any(o => o.ProductId == item.ProductId))
                {
                    OrderProductAssoc orderItem = order.FirstOrDefault(o => o.ProductId == item.ProductId);
                    orderItem.Qty = item.Qty;
                }
                else
                {
                    OrderProductAssoc newAssoc = new OrderProductAssoc()
                    {
                        ProductId = item.ProductId,
                        OrderId = (int)HttpContext.Session.GetInt32("OrderId"),
                        Qty = item.Qty
                    };
                    _context.Add(newAssoc);
                }
            }
            _context.SaveChanges();
        }

        return RedirectToAction("Order");
    }

    //Render PickUp and Comments form
    [HttpGet("shop/checkout")]
    public IActionResult Order()
    {
        ViewBag.Addresses = new List<Address>();

        if (HttpContext.Session.GetInt32("UUID") != null)
        {
            ViewBag.Addresses = _context.Addresses.Where(a => a.UserId == HttpContext.Session.GetInt32("UUID")).ToList();
        }
        return View("OrderDetails");
    }

    //Submit PickUp and Comments, redirect to Shipping Address form
    [HttpPost("shop/checkout")]
    public IActionResult Details(bool PickUp, string OrderComments)
    {
        Order order = _context.Orders.FirstOrDefault(o => o.OrderId == HttpContext.Session.GetInt32("OrderId"));
        order.PickUp = PickUp;
        order.OrderComments = OrderComments;
        if(order.UserId == null && HttpContext.Session.GetInt32("UUID")!=null)
        {
            order.UserId = (int)HttpContext.Session.GetInt32("UUID");
        }
        _context.SaveChanges();
        if (order.PickUp == true)
        {
            return RedirectToAction("Billing");
        }

        return RedirectToAction("Shipping");
    }

    //Shipping Address GET - Render View of Shipping Address form. 
    [HttpGet("shop/order/shipping")]
    public IActionResult Shipping()
    {
        ViewBag.Addresses = new List<Address>();
        if (HttpContext.Session.GetInt32("UUID") != null)
        {
            ViewBag.Addresses = _context.Addresses.Where(a => a.UserId == HttpContext.Session.GetInt32("UUID")).ToList();
        }
        return View("ShippingAddress");
    }
    //Shipping Address POST - Update Order BillingAddressId, ShippingAddressId, PickUp, and OrderComments. Redirect to ReviewOrder.
    [HttpPost("shop/order/shipping")]
    public IActionResult SetShipping(int ship, bool UseAsBilling)
    {
        if (!ModelState.IsValid)
        {
            return Shipping();
        }

        Order order = _context.Orders.FirstOrDefault(o => o.OrderId == HttpContext.Session.GetInt32("OrderId"));
        order.ShippingAddressId = ship;

        if (UseAsBilling == true)
        {
            order.BillingAddressId = ship;
            _context.SaveChanges();
            return RedirectToAction("ReviewOrder");
        }

        _context.SaveChanges();
        return RedirectToAction("Billing");
    }

    //Billing Address GET - Render View of Billing Address form. 
    [HttpGet("shop/order/Billing")]
    public IActionResult Billing()
    {
        ViewBag.Addresses = new List<Address>();
        if (HttpContext.Session.GetInt32("UUID") != null)
        {
            ViewBag.Addresses = _context.Addresses.Where(a => a.UserId == HttpContext.Session.GetInt32("UUID")).ToList();
        }
        return View("BillingAddress");
    }
    //Billing Address POST  - Update Order BillingAddressId, BillingAddressId, PickUp, and OrderComments. Redirect to ReviewOrder.
    [HttpPost("shop/order/Billing")]
    public IActionResult SetBilling(int bill)
    {
        if (!ModelState.IsValid)
        {
            return Billing();
        }
        Order order = _context.Orders.FirstOrDefault(o => o.OrderId == HttpContext.Session.GetInt32("OrderId"));

        order.BillingAddressId = bill;
        _context.SaveChanges();

        return RedirectToAction("ReviewOrder");
    }

    //ReviewOrder Get ("shop/checkout/review") - ViewModel: Order-> OPAssocs -> Product. Return View.
    [HttpGet("shop/checkout/review")]
    public IActionResult ReviewOrder()
    {
        Order order = _context.Orders.Include(o => o.OrderProductAssocs).ThenInclude(opa => opa.Product).FirstOrDefault(o => o.OrderId == HttpContext.Session.GetInt32("OrderId"));

        ViewBag.Shipping = _context.Addresses.FirstOrDefault(a => a.AddressId == order.ShippingAddressId);
        ViewBag.Billing = _context.Addresses.FirstOrDefault(a => a.AddressId == order.BillingAddressId);

        return View("ReviewOrder", order);
    }

    //SubmitOrder POST ("shop/checkout/submit") - Dump Cart. For now, maybe send email then Redirect to order completion page


    //OrderComplete GET ("shop/checkout/complete") - Render Order complete page


    [HttpPost("addresses/new")]
    public IActionResult CreateAddress(Address newAddress)
    {
        if (ModelState.IsValid)
        {
            newAddress.UserId = (int)HttpContext.Session.GetInt32("UUID");
            _context.Add(newAddress);
            _context.SaveChanges();
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }

    //Add a new address as the shipping address through the shipping address form
    [HttpPost("addresses/newshipping")]
    public IActionResult NewShipping(formObj newAddress)
    {
        if (!ModelState.IsValid)
        {
            return Shipping();
        }
        if (HttpContext.Session.GetInt32("UUID") != null)
        {
            newAddress.formAddress.UserId = (int)HttpContext.Session.GetInt32("UUID");
        }
        _context.Add(newAddress.formAddress);
        _context.SaveChanges();
        Order order = _context.Orders.FirstOrDefault(o => o.OrderId == HttpContext.Session.GetInt32("OrderId"));
        order.ShippingAddressId = newAddress.formAddress.AddressId;

        if (newAddress.UseAsBilling == true)
        {
            order.BillingAddressId = newAddress.formAddress.AddressId;
            _context.SaveChanges();
            return RedirectToAction("ReviewOrder");
        }

        _context.SaveChanges();
        return RedirectToAction("Billing");
    }

    //Add a new address as the billing address through the form
    [HttpPost("addresses/newbilling")]
    public IActionResult NewBilling(Address newAddress)
    {
        if (!ModelState.IsValid)
        {
            return Billing();
        }
        if (HttpContext.Session.GetInt32("UUID") != null)
        {
            newAddress.UserId = (int)HttpContext.Session.GetInt32("UUID");
        }
        _context.Add(newAddress);
        _context.SaveChanges();
        Order order = _context.Orders.FirstOrDefault(o => o.OrderId == HttpContext.Session.GetInt32("OrderId"));

        order.BillingAddressId = newAddress.AddressId;
        _context.SaveChanges();

        return RedirectToAction("ReviewOrder");
    }

    //Update Cart
    [HttpPost("cart/{ItemId}/update")]
    public IActionResult UpdateCart(CartProductAssoc newItem, int ItemId)
    {
        if (!ModelState.IsValid)
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }

        CartProductAssoc? OldItem = _context.CartProductAssocs.FirstOrDefault(i => i.CartProductAssocId == ItemId);

        if (OldItem == null)
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        OldItem.Qty = newItem.Qty;
        OldItem.UpdatedAt = DateTime.Now;

        _context.SaveChanges();

        int cartId = (int)HttpContext.Session.GetInt32("CartId");
        int cartCount = _context.CartProductAssocs.Where(c => c.CartId == cartId).Sum(c => c.Qty);
        HttpContext.Session.SetInt32("CartCount", cartCount);

        return Redirect(Request.Headers["Referer"].ToString());

    }


    //Remove item from cart
    [HttpPost("cart/{ItemId}/delete")]
    public IActionResult RemoveFromCart(int ItemId)
    {
        CartProductAssoc? item = _context.CartProductAssocs.FirstOrDefault(d => d.CartProductAssocId == ItemId);

        if (item != null)
        {
            _context.CartProductAssocs.Remove(item);
            _context.SaveChanges();
        }

        int cartId = (int)HttpContext.Session.GetInt32("CartId");
        int cartCount = _context.CartProductAssocs.Where(c => c.CartId == cartId).Sum(c => c.Qty);
        HttpContext.Session.SetInt32("CartCount", cartCount);

        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpPost("order/complete/{OrderId}")]
    public IActionResult CompleteOrder(int OrderId)
    {
        Order order = _context.Orders.FirstOrDefault(o => o.OrderId == OrderId);
        Cart cart = _context.Carts.FirstOrDefault(c => c.CartId == order.CartId);
        _context.Carts.Remove(cart);
        _context.SaveChanges();
        HttpContext.Session.Remove("OrderId");
        HttpContext.Session.Remove("CartId");
        HttpContext.Session.Remove("CartCount");
        return RedirectToAction("ThankYou");
    }

    [HttpGet("order/complete")]
    public IActionResult ThankYou()
    {
        return View("OrderComplete");
    }
}

