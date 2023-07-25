using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaintBarnabasHouse.Models;

namespace SaintBarnabasHouse.Controllers;

public class BlogController : Controller
{
    private readonly ILogger<BlogController> _logger;

    private MyContext _context;


    public BlogController(ILogger<BlogController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;

    }

    [HttpGet("blog")]
    public IActionResult Blog()
    {
        List<BlogPost> AllBlogs = _context.BlogPosts.ToList();
        return View("Blog",AllBlogs);
    }

    [HttpGet("blog/{BlogPostId}")]
    public IActionResult BlogPost(int BlogPostId)
    {
        BlogPost? post = _context.BlogPosts.Include(b => b.Comments).ThenInclude(c => c.User).FirstOrDefault(b => b.BlogPostId == BlogPostId);
        if (post != null)
        {
            ViewBag.Creator = _context.Users.FirstOrDefault(u => u.UserId == post.Creator);
            return View("BlogPost", post);
        }
        return RedirectToAction("Blog");
    }

    [HttpGet("blog/new")]
    public IActionResult NewPost()
    {
        return View("NewPost");
    }

    [HttpPost("blog/new")]
    public IActionResult CreateNewPost(BlogPost newPost)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newPost);
            _context.SaveChanges();
            return RedirectToAction("BlogPost", newPost.BlogPostId);
        }
        else
        {
            return View("NewPost");
        }
    }

    [HttpPost("blog/newcomment")]
    public IActionResult NewComment(Comment newComment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newComment);
            _context.SaveChanges();
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }

}