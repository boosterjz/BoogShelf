using Microsoft.EntityFrameworkCore;

namespace BookShelf.Models {
  public class BookShelfDbContext : DbContext {
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public BookShelfDbContext(DbContextOptions options) : base(options) {}
  }
}