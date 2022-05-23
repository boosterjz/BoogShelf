using BookShelf.Models;
using BookShelf.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Controllers;

public class HomeController : Controller {
    private readonly IStoreRepository _repository;
    public int PageSize = 4;

    public HomeController(IStoreRepository repo) {
        _repository = repo;
    }

    public ViewResult Index(string? category, int productPage = 1)
        => View(new ProductsListViewModel {
            Products = _repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null
                    ? _repository.Products.Count()
                    : _repository.Products.Count(e => e.Category == category)
            },
            CurrentCategory = category
        });

    public ViewResult Genre(string? genre, int productPage = 1) => 
        View("Index", new ProductsListViewModel {
            Products = _repository.Products
                .Where(p => genre == null || p.Genre == genre)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = genre == null
                    ? _repository.Products.Count()
                    : _repository.Products.Count(e => e.Genre == genre)
            },
            CurrentGenre = genre
        });
    
    public ViewResult Author(string? author, int productPage = 1) => 
        View("Index", new ProductsListViewModel {
            Products = _repository.Products
                .Where(p => author == null || p.Author == author)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = author == null
                    ? _repository.Products.Count()
                    : _repository.Products.Count(e => e.Author == author)
            },
            CurrentAuthor = author
        });
}