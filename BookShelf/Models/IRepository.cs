using System.Linq;

namespace BookShelf.Models {
    public interface IRepository {
        IQueryable<Author> Authors { get; }
        IQueryable<Book> Books { get; }
    }
}