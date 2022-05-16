using Microsoft.AspNetCore.Mvc;
using BookShelf.Models;

namespace BookShelf.Components {

    public class NavigationMenuViewComponent : ViewComponent {
        private IRepository _repository;

        public NavigationMenuViewComponent(IRepository repository) {
            _repository = repository;
        }

        public IViewComponentResult Invoke() {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}