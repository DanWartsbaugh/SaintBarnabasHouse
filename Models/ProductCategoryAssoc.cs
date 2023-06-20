#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SaintBarnabasHouse.Models;
public class ProductCategoryAssoc
{
    [Key]
    [Required]
    public int ProductCategoryAssocId { get; set; }

    //Relationships to Product and Category
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}

