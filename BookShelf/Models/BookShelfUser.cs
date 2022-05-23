using Microsoft.AspNetCore.Identity;

namespace BookShelf.Models;

public class BookShelfUser : IdentityUser
{
    public List<Order> Orders { get; set; } = new ();

    public BookShelfUser(string userName) : base(userName)
    {
    }
}