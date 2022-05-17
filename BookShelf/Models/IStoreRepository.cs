using System.Linq;

namespace BookShelf.Models {
    public interface IStoreRepository {
        IQueryable<Author> Authors { get; }
        IQueryable<Book> Books { get; }
        
        void SaveBook(Book b);
        void CreateBook(Book b);
        void DeleteBook(Book b);
        
        void SaveAuthor(Author a);
        void CreateAuthor(Author a);
        void DeleteAuthor(Author a);
    }
}