using Moq;
using BookShelf.Models;
using BookShelf.Pages;
using System.Linq;
using Xunit;

namespace BookShelf.Test {
    public class CartPageTests {
        [Fact]
        public void CanLoadCart() {
            var b1 = new Book {BookId = 1, Title = "Book 1"};
            var b2 = new Book {BookId = 2, Title = "Book 2"};
            var mockedRepository = new Mock<IRepository>();
            mockedRepository.Setup(r => r.Books).Returns(new Book[] {
                b1, b2
            }.AsQueryable());

            var testCart = new Cart();
            testCart.AddItem(b1, 2);
            testCart.AddItem(b2, 1);

            var cartModel = new CartModel(mockedRepository.Object, testCart);
            cartModel.OnGet("myUrl");

            Assert.Equal(2, cartModel.Cart.Lines.Count());
            Assert.Equal("myUrl", cartModel.ReturnUrl);
        }
    }
}