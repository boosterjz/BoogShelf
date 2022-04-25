using Microsoft.EntityFrameworkCore;

namespace BookShelf.Models {
    public class BookShelfRepository : IRepository {
        private BookShelfDbContext _dbContext;

        public BookShelfRepository(BookShelfDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IQueryable<Author> Authors => _dbContext.Authors.Include(a => a.Books);
        public IQueryable<Book> Books => _dbContext.Books.Include(b => b.Author);
    }
}