using Microsoft.AspNetCore.Mvc;
using BookShelf.Models;

namespace BookShelf.Controllers {

    public class OrderController : Controller {
        private readonly IOrderRepository _repository;
        private readonly Cart _cart;

        public OrderController(IOrderRepository repoService, Cart cartService) {
            _repository = repoService;
            _cart = cartService;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order) {
            if (!_cart.Lines.Any()) {
                ModelState.AddModelError("", "Корзина пуста");
            }
            if (ModelState.IsValid) {
                order.Lines = _cart.Lines.ToArray();
                _repository.SaveOrder(order);
                _cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }

            return View();
        }
    }
}