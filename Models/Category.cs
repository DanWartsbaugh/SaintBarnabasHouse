#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SaintBarnabasHouse.Models;
public class Category
{
    [Key]
    [Required]
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relationship properties
    public List<ProductCategoryAssoc> ProductCategoryAssocs { get; set; } = new List<ProductCategoryAssoc>();
}

