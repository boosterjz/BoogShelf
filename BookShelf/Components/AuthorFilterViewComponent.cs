using BookShelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Components;

public class AuthorFilterViewComponent : ViewComponent {
    private readonly IStoreRepository _repository;

    public AuthorFilterViewComponent(IStoreRepository repo) {
        _repository = repo;
    }

    public IViewComponentResult Invoke() {
        ViewBag.SelectedAuthor = RouteData.Values["author"] ?? "";
        return View(_repository.Products
            .Select(x => x.Author)
            .Distinct()
            .OrderBy(x => x));
    }
}