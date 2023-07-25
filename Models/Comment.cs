#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaintBarnabasHouse.Models;
public class Comment
{
    [Key]
    public int CommentId { get; set; }

    [Required]
    public string Text { get; set; }



    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relationships
    [Required]
    public int UserId { get; set; }

    public User? User { get; set; }

    public int? BlogPostId { get; set; }
    public int? ProductId { get; set; }
    public int? ParentComment { get; set; }
}

