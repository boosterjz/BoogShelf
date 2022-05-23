using System.ComponentModel.DataAnnotations;

namespace BookShelf.Models.ViewModels;

public class RegisterModel
{
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
    [Required]
    public string? PasswordConfirm { get; set; }
    
    [Required]
    public bool Remember { get; set; }

    public string ReturnUrl { get; set; } = "/";
}