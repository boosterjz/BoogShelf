using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookShelf.Models;
using BookShelf.Models.ViewModels;
using NLog;

namespace BookShelf.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository _repository;
    public int PageSize = 4;

    public HomeController(ILogger<HomeController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public ViewResult Index(int page = 1)
    {
        return View(new BooksListViewModel() {
            Books = _repository.Books.OrderBy(b => b.BookId).Skip((page - 1) * PageSize).Take(PageSize),
            PagingInfo = new PagingInfo() {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = _repository.Books.Count()
            }
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
