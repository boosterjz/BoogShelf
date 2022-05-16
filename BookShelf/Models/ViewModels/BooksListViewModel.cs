﻿namespace BookShelf.Models.ViewModels; 

public class BooksListViewModel {
    public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
    public PagingInfo PagingInfo { get; set; } = new();
    public string? CurrentCategory { get; set; }
    public string? CurrentAuthor { get; set; }
}