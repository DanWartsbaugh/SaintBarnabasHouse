using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaintBarnabasHouse.Models;

namespace SaintBarnabasHouse.Controllers;

public class EventController : Controller
{
    private readonly ILogger<EventController> _logger;

    private MyContext _context;


    public EventController(ILogger<EventController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;

    }

    [HttpGet("event")]
    public IActionResult Event()
    {
        List<Event> UpcomingEvents = _context.Events.Where(e => e.Status > 0).OrderBy(e => e.Date).ToList();
        return View("Event", UpcomingEvents);
    }

    [HttpGet("event/request")]
    public IActionResult EventRequest()
    {
        return View("EventRequest");
    }

    [HttpPost("event/request")]
    public IActionResult SubmitEventRequest(Event request)
    {
        if (ModelState.IsValid)
        {
            request.Status = 0;
            _context.Add(request);
            _context.SaveChanges();
            return RedirectToAction("Event");
        }
        else
        {
            return View("EventRequest");
        }
    }

    [HttpGet("event/{EventId}")]
    public IActionResult ShowEvent(int EventId)
    {
        Event item = _context.Events.FirstOrDefault(e => e.EventId == EventId);
        if (item == null)
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        return View("SingleEvent", item);
    }

    [HttpGet("event/{EventId}/update")]
    public IActionResult RenderUpdateEvent(int EventId)
    {
        Event? item = _context.Events.FirstOrDefault(e => e.EventId == EventId);
        if (item == null)
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        return View("UpdateEvent", item);
    }

    [HttpPost("event/{EventId}/update")]
    public IActionResult UpdateEvent(Event updatedEvent, int EventId)
    {
        if (HttpContext.Session.GetString("Admin") == "Admin")
        {
            if (!ModelState.IsValid)
            {
                return RenderUpdateEvent(EventId);
            }

            Event? OldEvent = _context.Events.FirstOrDefault(e => e.EventId == EventId);

            if (OldEvent == null)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            OldEvent.EventType = updatedEvent.EventType;
            OldEvent.Private = updatedEvent.Private;
            OldEvent.OnSite = updatedEvent.OnSite;
            OldEvent.Address = updatedEvent.Address;
            OldEvent.RequesterName = updatedEvent.RequesterName;
            OldEvent.RequesterEmail = updatedEvent.RequesterEmail;
            OldEvent.Details = updatedEvent.Details;
            OldEvent.Date = updatedEvent.Date;
            OldEvent.Description = updatedEvent.Description;
            OldEvent.Status = updatedEvent.Status;
            OldEvent.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("Dashboard", "User");
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpPost("event/{EventId}/updatestatus")]
    public IActionResult UpdateStatus(int EventId, int Status)
    {
        if (HttpContext.Session.GetString("Admin") == "Admin")
        {
            Event? item = _context.Events.FirstOrDefault(e => e.EventId == EventId);
            if (item != null)
            {
                item.Status = Status;
                item.UpdatedAt = DateTime.Now;
                _context.SaveChanges();

                return RedirectToAction("Dashboard", "User");
            }
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }

    // [HttpGet("blog/{BlogPostId}")]
    // public IActionResult BlogPost(int BlogPostId)
    // {
    //     BlogPost? post = _context.BlogPosts.Include(b => b.Comments).ThenInclude(c => c.User).FirstOrDefault(b => b.BlogPostId == BlogPostId);
    //     if (post != null)
    //     {
    //         ViewBag.Creator = _context.Users.FirstOrDefault(u => u.UserId == post.Creator);
    //         return View("BlogPost", post);
    //     }
    //     return RedirectToAction("Blog");
    // }

    // [HttpGet("blog/new")]
    // public IActionResult NewPost()
    // {
    //     return View("NewPost");
    // }

    // [HttpPost("blog/new")]
    // public IActionResult CreateNewPost(BlogPost newPost)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         _context.Add(newPost);
    //         _context.SaveChanges();
    //         return RedirectToAction("BlogPost", newPost.BlogPostId);
    //     }
    //     else
    //     {
    //         return View("NewPost");
    //     }
    // }

    // [HttpGet("blog/{BlogPostId}/update")]
    // public IActionResult RenderUpdatePost(int BlogPostId)
    // {
    //     if (HttpContext.Session.GetString("Admin") == "Admin")
    //     {
    //         BlogPost? post = _context.BlogPosts.FirstOrDefault(b => b.BlogPostId == BlogPostId);
    //         if (post != null)
    //         {
    //             return View("UpdatePost", post);
    //         }
    //     }

    //     return Redirect(Request.Headers["Referer"].ToString());
    // }



    // [HttpPost("blog/newcomment")]
    // public IActionResult NewComment(Comment newComment)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         _context.Add(newComment);
    //         _context.SaveChanges();
    //     }

    //     return Redirect(Request.Headers["Referer"].ToString());
    // }

    // [HttpPost("blog/{BlogPostId}/remove")]
    // public IActionResult RemovePost(int BlogPostId)
    // {
    //     if (HttpContext.Session.GetString("Admin") == "Admin")
    //     {
    //         BlogPost? post = _context.BlogPosts.FirstOrDefault(b => b.BlogPostId == BlogPostId);
    //         if (post != null)
    //         {
    //             _context.BlogPosts.Remove(post);
    //             _context.SaveChanges();
    //         }
    //     }

    //     return Redirect(Request.Headers["Referer"].ToString());

    // }

    // [HttpPost("blog/{CommentId}/removecomment")]
    // public IActionResult RemoveComment(int CommentId)
    // {
    //     if (HttpContext.Session.GetString("Admin") == "Admin")
    //     {
    //         Comment? comment = _context.Comments.FirstOrDefault(c => c.CommentId == CommentId);
    //         if (comment != null)
    //         {
    //             _context.Comments.Remove(comment);
    //             _context.SaveChanges();
    //         }
    //     }

    //     return Redirect(Request.Headers["Referer"].ToString());

    // }
}