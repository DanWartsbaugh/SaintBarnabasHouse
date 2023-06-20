#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaintBarnabasHouse.Models;
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Display(Name="Main Category")]
    public string MainCat { get; set; }
    
    [Required]
    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relationships
    public List<Image> Images { get; set; } = new();
    public List<ProductCategoryAssoc> ProductCategoryAssocs {get; set;} = new();
}

