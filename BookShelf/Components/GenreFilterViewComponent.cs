using BookShelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Components;

public class GenreFilterViewComponent : ViewComponent {
    private readonly IStoreRepository _repository;

    public GenreFilterViewComponent(IStoreRepository repo) {
        _repository = repo;
    }

    public IViewComponentResult Invoke() {
        ViewBag.SelectedGenre = RouteData.Values["genre"] ?? "";
        return View(_repository.Products
            .Select(x => x.Genre)
            .Distinct()
            .OrderBy(x => x));
    }
}