#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SaintBarnabasHouse.Models;
public class Cart
{
    [Key]
    [Required]
    public int CartId { get; set; }

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relationship properties
    public List<CartProductAssoc> CartProductAssocs { get; set; } = new List<CartProductAssoc>();
}

