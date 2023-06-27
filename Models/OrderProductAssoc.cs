#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SaintBarnabasHouse.Models;
public class OrderProductAssoc
{
    [Key]
    [Required]
    public int OrderProductAssocId { get; set; }

    [Required]
    public int Qty { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relationships to Product and Category
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }
}

