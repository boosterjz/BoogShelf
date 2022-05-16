using System.Linq;

namespace BookShelf.Models {
    public interface IStoreRepository {
        IQueryable<Author> Authors { get; }
        IQueryable<Book> Books { get; }
    }
}