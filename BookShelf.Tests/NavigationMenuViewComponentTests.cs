using System.Collections.Generic;
using System.Linq;
using BookShelf.Components;
using BookShelf.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Xunit;

namespace BookShelf.Tests; 

public class NavigationMenuViewComponentTests {
    [Fact]
    public void Can_Select_Categories() {
        var mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns(new[] {
            new Product {ProductId = 1, Title = "P1",
                Category = "Apples"},
            new Product {ProductId = 2, Title = "P2",
                Category = "Apples"},
            new Product {ProductId = 3, Title = "P3",
                Category = "Plums"},
            new Product {ProductId = 4, Title = "P4",
                Category = "Oranges"},
        }.AsQueryable());

        var target =
            new NavigationMenuViewComponent(mock.Object);

        var results = ((IEnumerable<string>?)(target.Invoke()
                           as ViewViewComponentResult)?.ViewData?.Model
                       ?? Enumerable.Empty<string>()).ToArray();

        Assert.True(new[] { "Apples",
            "Oranges", "Plums" }.SequenceEqual(results));
    }

    [Fact]
    public void Indicates_Selected_Category() {
        const string categoryToSelect = "Apples";
        var mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns(new[] {
            new Product {ProductId = 1, Title = "P1", Category = "Apples"},
            new Product {ProductId = 4, Title = "P2", Category = "Oranges"},
        }.AsQueryable());

        var target = new NavigationMenuViewComponent(mock.Object) {
            ViewComponentContext = new ViewComponentContext {
                ViewContext = new ViewContext {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            }
        };
        target.RouteData.Values["category"] = categoryToSelect;

        var result = (string?)(target.Invoke()
            as ViewViewComponentResult)?.ViewData?["SelectedCategory"];

        Assert.Equal(categoryToSelect, result);
    }
}