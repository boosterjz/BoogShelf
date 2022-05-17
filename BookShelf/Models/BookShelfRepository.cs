using Microsoft.EntityFrameworkCore;

namespace BookShelf.Models {
    public class BookShelfRepository : IStoreRepository {
        private BookShelfDbContext _dbContext;

        public BookShelfRepository(BookShelfDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IQueryable<Author> Authors => _dbContext.Authors.Include(a => a.Books);
        public IQueryable<Book> Books => _dbContext.Books.Include(b => b.Author);

        public void CreateBook(Book b) {
            _dbContext.Add(b);
            _dbContext.SaveChanges();
        }

        public void DeleteBook(Book b) {
            _dbContext.Remove(b);
            _dbContext.SaveChanges();
        }
        
        public void CreateAuthor(Author a) {
            _dbContext.Add(a);
            _dbContext.SaveChanges();
        }

        public void DeleteAuthor(Author a) {
            _dbContext.Remove(a);
            _dbContext.SaveChanges();
        }

        public void SaveBook(Book b) {
            _dbContext.SaveChanges();
        }

        public void SaveAuthor(Author a) {
            _dbContext.SaveChanges();
        }
    }
}