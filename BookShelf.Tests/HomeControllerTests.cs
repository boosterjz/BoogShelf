using System.Linq;
using BookShelf.Controllers;
using BookShelf.Models;
using BookShelf.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookShelf.Tests; 

public class HomeControllerTests {
    [Fact]
    public void Can_Use_Repository() {
        var mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns(new[] {
            new Product {ProductId = 1, Title = "P1"},
            new Product {ProductId = 2, Title = "P2"}
        }.AsQueryable());

        var controller = new HomeController(mock.Object);

        var result =
            controller.Index(null).ViewData.Model
                as ProductsListViewModel ?? new ProductsListViewModel();

        var prodArray = result.Products.ToArray();
        Assert.True(prodArray.Length == 2);
        Assert.Equal("P1", prodArray[0].Title);
        Assert.Equal("P2", prodArray[1].Title);
    }

    [Fact]
    public void Can_Paginate() {
        var mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns(new[] {
            new Product {ProductId = 1, Title = "P1"},
            new Product {ProductId = 2, Title = "P2"},
            new Product {ProductId = 3, Title = "P3"},
            new Product {ProductId = 4, Title = "P4"},
            new Product {ProductId = 5, Title = "P5"}
        }.AsQueryable());

        var controller = new HomeController(mock.Object);
        controller.PageSize = 3;

        var result =
            controller.Index(null, 2).ViewData.Model
                as ProductsListViewModel ?? new ProductsListViewModel();

        var prodArray = result.Products.ToArray();
        Assert.True(prodArray.Length == 2);
        Assert.Equal("P4", prodArray[0].Title);
        Assert.Equal("P5", prodArray[1].Title);
    }

    [Fact]
    public void Can_Send_Pagination_View_Model() {
        var mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns(new[] {
            new Product {ProductId = 1, Title = "P1"},
            new Product {ProductId = 2, Title = "P2"},
            new Product {ProductId = 3, Title = "P3"},
            new Product {ProductId = 4, Title = "P4"},
            new Product {ProductId = 5, Title = "P5"}
        }.AsQueryable());

        var controller =
            new HomeController(mock.Object) { PageSize = 3 };

        var result =
            controller.Index(null, 2).ViewData.Model as
                ProductsListViewModel ?? new ProductsListViewModel();

        var pageInfo = result.PagingInfo;
        Assert.Equal(2, pageInfo.CurrentPage);
        Assert.Equal(3, pageInfo.ItemsPerPage);
        Assert.Equal(5, pageInfo.TotalItems);
        Assert.Equal(2, pageInfo.TotalPages);
    }

    [Fact]
    public void Can_Filter_Products() {
        var mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns(new[] {
            new Product {ProductId = 1, Title = "P1", Category = "Cat1"},
            new Product {ProductId = 2, Title = "P2", Category = "Cat2"},
            new Product {ProductId = 3, Title = "P3", Category = "Cat1"},
            new Product {ProductId = 4, Title = "P4", Category = "Cat2"},
            new Product {ProductId = 5, Title = "P5", Category = "Cat3"}
        }.AsQueryable());

        var controller = new HomeController(mock.Object);
        controller.PageSize = 3;

        var result = (controller.Index("Cat2").ViewData.Model
            as ProductsListViewModel ?? new ProductsListViewModel()).Products.ToArray();

        Assert.Equal(2, result.Length);
        Assert.True(result[0].Title == "P2" && result[0].Category == "Cat2");
        Assert.True(result[1].Title == "P4" && result[1].Category == "Cat2");
    }


    [Fact]
    public void Generate_Category_Specific_Product_Count() {
        var mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns(new[] {
            new Product {ProductId = 1, Title = "P1", Category = "Cat1"},
            new Product {ProductId = 2, Title = "P2", Category = "Cat2"},
            new Product {ProductId = 3, Title = "P3", Category = "Cat1"},
            new Product {ProductId = 4, Title = "P4", Category = "Cat2"},
            new Product {ProductId = 5, Title = "P5", Category = "Cat3"}
        }.AsQueryable());

        var target = new HomeController(mock.Object);
        target.PageSize = 3;

        ProductsListViewModel? GetModel(ViewResult result) => result.ViewData.Model as ProductsListViewModel;

        var res1 = GetModel(target.Index("Cat1"))?.PagingInfo.TotalItems;
        var res2 = GetModel(target.Index("Cat2"))?.PagingInfo.TotalItems;
        var res3 = GetModel(target.Index("Cat3"))?.PagingInfo.TotalItems;
        var resAll = GetModel(target.Index(null))?.PagingInfo.TotalItems;

        Assert.Equal(2, res1);
        Assert.Equal(2, res2);
        Assert.Equal(1, res3);
        Assert.Equal(5, resAll);
    }
}