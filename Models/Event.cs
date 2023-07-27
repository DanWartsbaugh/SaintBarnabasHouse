#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace SaintBarnabasHouse.Models;
public class Event
{
    [Key]
    public int EventId { get; set; }

    [Required]
    public string EventType { get; set; }

    [Required]
    public bool Private { get; set; }

    [Required]
    public bool OnSite { get; set; }

    public string? Address { get; set; }

    [Required]
    public string RequesterName { get; set; }

    [Required]
    [EmailAddress]
    public string RequesterEmail { get; set; }

    [Required]
    public string Details { get; set; }

    [Required]
    public DateTime Date { get; set; }

// Statuses will be -1: Past, 0: Requested, 1: Confirmed, 2: Cancelled
    public int Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}

