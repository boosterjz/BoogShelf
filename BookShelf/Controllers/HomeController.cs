using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookShelf.Models;
using BookShelf.Models.ViewModels;
using NLog;

namespace BookShelf.Controllers;

public class HomeController : Controller
{
    private readonly IRepository _repository;
    public int PageSize = 4;

    public HomeController(IRepository repository)
    {
        _repository = repository;
    }

    public ViewResult Index(string? category, int page = 1) =>
        View(new BooksListViewModel() {
            Books = _repository.Books
                .Where(b => category == null || b.Category == category)
                .OrderBy(b => b.BookId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo() {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = category == null
                    ? _repository.Books.Count()
                    : _repository.Books
                        .Where(b => b.Category == category)
                        .Count()
            },
            CurrentCategory = category
        });
}
