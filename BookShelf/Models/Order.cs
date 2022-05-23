using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookShelf.Models;

public class Order {

    [BindNever]
    public int OrderId { get; set; }
    [BindNever]
    public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

    [Required(ErrorMessage = "Please enter a name")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter the first address line")]
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Apartment { get; set; }

    [Required(ErrorMessage = "Please enter a city name")]
    public string? City { get; set; }

    public string? Zip { get; set; }

    [Required(ErrorMessage = "Please enter a country name")]
    public string? Country { get; set; }

    [BindNever]
    public bool Shipped { get; set; }

    [BindNever]
    public string UserId { get; set; } = "";
}