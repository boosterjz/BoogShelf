namespace BookShelf.Models.ViewModels; 

public class BooksListViewModel {
    public IEnumerable<Book> Books { get; set; }
    public PagingInfo PagingInfo { get; set; }
}