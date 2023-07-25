#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaintBarnabasHouse.Models;
public class BlogPost
{
    [Key]
    public int BlogPostId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [Display(Name = "Main Image URL")]
    public string? MainImgUrl { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relationships
    public int Creator { get; set; }
    public List<Comment> Comments { get; set; } = new();

}

