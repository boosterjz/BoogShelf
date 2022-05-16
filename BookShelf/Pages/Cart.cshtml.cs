using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShelf.Infrastructure;
using BookShelf.Models;

namespace BookShelf.Pages {
    public class CartModel : PageModel {
        private IStoreRepository _repository;
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public CartModel(IStoreRepository repository, Cart cartService) {
            _repository = repository;
            Cart = cartService;
        }

        public void OnGet(string returnUrl) {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(ulong bookId, string returnUrl) {
            var book = _repository.Books
                .FirstOrDefault(b => b.BookId == bookId);
            
            if (book != null) {
                Cart.AddItem(book, 1);
            }

            return RedirectToPage(new { returnUrl });
        }

        public IActionResult OnPostRemove(ulong bookId, string returnUrl) {
            Cart.RemoveLine(Cart.Lines.First(cl =>
                cl.Book.BookId == bookId).Book);
            return RedirectToPage(new { returnUrl });
        }
    }
}