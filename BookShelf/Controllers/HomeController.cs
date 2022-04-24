using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookShelf.Models;
using NLog;

namespace BookShelf.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository _repository;

    public HomeController(ILogger<HomeController> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public IActionResult Index()
    {
        return View(_repository.Books);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
