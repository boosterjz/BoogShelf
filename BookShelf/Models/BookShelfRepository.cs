using System.Linq;

namespace BookShelf.Models {
    public class BookShelfRepository : IRepository {
        private BookShelfDbContext _dbContext;

        public BookShelfRepository(BookShelfDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IQueryable<Author> Authors => _dbContext.Authors;
        public IQueryable<Book> Books => _dbContext.Books;
    }
}