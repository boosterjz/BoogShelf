using Microsoft.EntityFrameworkCore;

namespace BookShelf.Models {
    public class BookShelfDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Order> Orders => Set<Order>();

        public BookShelfDbContext(DbContextOptions<BookShelfDbContext> options) 
            : base(options) { }
    }
}