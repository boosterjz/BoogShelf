using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using BookShelf.Components;
using BookShelf.Models;
using Xunit;

namespace BookShelf.Tests {

    public class NavigationMenuViewComponentTests {

        [Fact]
        public void CanSelectCategories() {
            var mock = new Mock<IRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new Book {BookId = 1, Title = "P1",
                    Category = "Apples"},
                new Book {BookId = 2, Title = "P2",
                    Category = "Apples"},
                new Book {BookId = 3, Title = "P3",
                    Category = "Plums"},
                new Book {BookId = 4, Title = "P4",
                    Category = "Oranges"},
            }.AsQueryable());

            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object);

            // Act = get the set of categories
            string[] results = ((IEnumerable<string>?)(target.Invoke()
               as ViewViewComponentResult)?.ViewData?.Model
                 ?? Enumerable.Empty<string>()).ToArray();

            // Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples",
                "Oranges", "Plums" }, results));
        }

        [Fact]
        public void Indicates_Selected_Category() {

            // Arrange
            string categoryToSelect = "Apples";
            var mock = new Mock<IRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new Book {BookId = 1, Title = "P1", Category = "Apples"},
                new Book {BookId = 4, Title = "P2", Category = "Oranges"},
            }.AsQueryable());

            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext {
                ViewContext = new ViewContext {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            // Action
            string? result = (string?)(target.Invoke()
                as ViewViewComponentResult)?.ViewData?["SelectedCategory"];

            // Assert
            Assert.Equal(categoryToSelect, result);
        }

    }
}