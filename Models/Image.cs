#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaintBarnabasHouse.Models;
public class Image
{
    [Key]
    public int ImageId {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public int? ProductId {get;set;}
}