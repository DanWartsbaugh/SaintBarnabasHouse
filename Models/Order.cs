#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SaintBarnabasHouse.Models;
public class Order
{
    [Key]
    [Required]
    public int OrderId { get; set; }


    public int? ShippingAddressId { get; set; }
    public int? BillingAddressId { get; set; }
    public int OrderStatus { get; set; } = 0;

    [Display(Name = "Comments")]
    public string? OrderComments { get; set; }

    [Display(Name = "Pick up")]
    public bool PickUp { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relationship properties
    public List<OrderProductAssoc> OrderProductAssocs { get; set; } = new List<OrderProductAssoc>();

    public int? UserId { get; set; }
    public User? User { get; set; }
}

