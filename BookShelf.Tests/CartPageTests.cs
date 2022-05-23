using System.Linq;
using BookShelf.Models;
using BookShelf.Pages;
using Moq;
using Xunit;

namespace BookShelf.Tests; 

public class CartPageTests {
    [Fact]
    public void Can_Load_Cart() {
        var p1 = new Product { ProductId = 1, Title = "P1" };
        var p2 = new Product { ProductId = 2, Title = "P2" };
        var mockRepo = new Mock<IStoreRepository>();
        mockRepo.Setup(m => m.Products).Returns(new[] {
            p1, p2
        }.AsQueryable());

        var testCart = new Cart();
        testCart.AddItem(p1, 2);
        testCart.AddItem(p2, 1);

        var cartModel = new CartModel(mockRepo.Object, testCart);
        cartModel.OnGet("myUrl");

        Assert.Equal(2, cartModel.Cart.Lines.Count);
        Assert.Equal("myUrl", cartModel.ReturnUrl);
    }

    [Fact]
    public void Can_Update_Cart() {
        var mockRepo = new Mock<IStoreRepository>();
        mockRepo.Setup(m => m.Products).Returns(new[] {
            new Product { ProductId = 1, Title = "P1" }
        }.AsQueryable());

        var testCart = new Cart();

        var cartModel = new CartModel(mockRepo.Object, testCart);
        cartModel.OnPost(1, "myUrl");

        Assert.Single(testCart.Lines);
        Assert.Equal("P1", testCart.Lines.First().Product.Title);
        Assert.Equal(1, testCart.Lines.First().Quantity);
    }
}