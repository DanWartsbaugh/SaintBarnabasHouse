#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SaintBarnabasHouse.Models;
public class ProductImageAssoc
{
    [Key]
    [Required]
    public int ProductImageAssocId { get; set; }

    //Relationships to Product and Image
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int ImageId { get; set; }
    public Image? Image { get; set; }
}

