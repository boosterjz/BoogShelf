using Microsoft.AspNetCore.Mvc;
using Moq;
using BookShelf.Controllers;
using BookShelf.Models;
using Xunit;

namespace BookShelf.Tests {

    public class OrderControllerTests {

        [Fact]
        public void CannotCheckoutEmptyCart() {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();
            var target = new OrderController(mock.Object, cart);

            var result = target.Checkout(order) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails() {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Book(), 1);
            var target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");

            ViewResult? result = target.Checkout(new Order()) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order() {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Book(), 1);
            var target = new OrderController(mock.Object, cart);

            RedirectToPageResult? result =
                    target.Checkout(new Order()) as RedirectToPageResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal("/Completed", result?.PageName);
        }
    }
}