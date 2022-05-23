using BookShelf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Controllers;

public class OrderController : Controller {
    private readonly IOrderRepository _repository;
    private readonly Cart _cart;
    private readonly UserManager<BookShelfUser> _userManager;

    public OrderController(IOrderRepository repoService, Cart cartService, UserManager<BookShelfUser> userManager) {
        _repository = repoService;
        _cart = cartService;
        _userManager = userManager;
    }

    [Authorize]
    public ViewResult Checkout() => View(new Order());

    [HttpPost]
    [Authorize]
    public IActionResult Checkout(Order order) {
        if (!_cart.Lines.Any()) {
            ModelState.AddModelError("", "Sorry, your cart is empty!");
        }

        if (!ModelState.IsValid) return View();
        
        order.Lines = _cart.Lines.ToArray();
        order.UserId = _userManager.FindByNameAsync(User.Identity?.Name).Result.Id;
        _repository.SaveOrder(order);
        _cart.Clear();
        return RedirectToPage("/Completed", new { orderId = order.OrderId });
    }
}