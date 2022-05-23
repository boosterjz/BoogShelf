using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Models;

public class AppIdentityDbContext : IdentityDbContext<BookShelfUser> {
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { }
}