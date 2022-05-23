using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShelf.Models;

public class Product {

    public long? ProductId { get; set; }

    [Required(ErrorMessage = "Please enter a product name")]
    public string Title { get; set; } = string.Empty;
        
    [Required(ErrorMessage = "Please enter author's name")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue,
        ErrorMessage = "Please enter a positive price")]
    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Please specify a category")]
    public string Category { get; set; } = string.Empty;
        
    [Required(ErrorMessage = "Please specify a genre")]
    public string Genre { get; set; } = string.Empty;
}