using BookShelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Controllers;

public class FilterController : Controller
{
    private readonly IStoreRepository _repository;

    public FilterController(IStoreRepository repository)
    {
        _repository = repository;
    }

    public ViewResult Categories()
    {
        return View(_repository.Products.Select(p => p.Category).Distinct().OrderBy(x => x));
    }

    public ViewResult Genres()
    {
        return View(_repository.Products.Select(p => p.Genre).Distinct().OrderBy(x => x));
    }

    public ViewResult Authors()
    {
        return View(_repository.Products.Select(p => p.Author).Distinct().OrderBy(x => x));
    }
}