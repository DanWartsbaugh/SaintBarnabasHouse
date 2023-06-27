using System.ComponentModel.DataAnnotations.Schema;

namespace SaintBarnabasHouse.Models;

[NotMapped]
public class formObj
{
    public Address formAddress{ get; set; }

    public bool UseAsBilling { get; set; }
}