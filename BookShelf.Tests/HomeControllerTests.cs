
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BookShelf.Controllers;
using BookShelf.Models;
using BookShelf.Models.ViewModels;
using Moq;
using Xunit;

namespace BookShelf.Tests {
    public class HomeControllerTest {
        [Fact]
        public void CanUseRepository() {
            // устанавливаем имитацию базы данных для контроллера
            var mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new() { BookId = 1, Title = "Book1" },
                new() { BookId = 2, Title = "Book2" },
            }.AsQueryable());
            // создаем коотроллер
            var controller = new HomeController(mock.Object);

            // получаем данные, переданные в контроллер
            var result = controller.Index(null)?.ViewData.Model as BooksListViewModel ?? new();

            // делаем проверки
            var resultArray = result.Books.ToArray();
            Assert.Equal(2, resultArray.Length);
            Assert.Equal("Book1", resultArray[0].Title);
            Assert.Equal("Book2", resultArray[1].Title);
        }

        [Fact]
        public void CanPaginate() {
            var mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new() {BookId = 1, Title = "b1"},
                new() {BookId = 2, Title = "b2"},
                new() {BookId = 3, Title = "b3"},
                new() {BookId = 4, Title = "b4"},
                new() {BookId = 5, Title = "b5"},
            }.AsQueryable());
            var controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            var result = controller.Index(null, 2)?.ViewData.Model as BooksListViewModel ?? new();

            var resultArray = result.Books.ToArray();
            Assert.Equal(2, resultArray.Length);
            Assert.Equal("b4", resultArray?[0].Title);
            Assert.Equal("b5", resultArray?[1].Title);
        }

        [Fact]
        public void CanSendPaginationViewModel() {
            var mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new() {BookId = 1, Title = "b1"},
                new() {BookId = 2, Title = "b2"},
                new() {BookId = 3, Title = "b3"},
                new() {BookId = 4, Title = "b4"},
                new() {BookId = 5, Title = "b5"}
            }.AsQueryable());
            var controller = new HomeController(mock.Object) {PageSize = 3};

            var result = controller.Index(null, 2)?.ViewData.Model as BooksListViewModel ?? new();

            var pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }

        [Fact]
        public void CanFilterBooks() {

            var mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new Book {BookId = 1, Title = "b1", Category = "Cat1"},
                new Book {BookId = 2, Title = "b2", Category = "Cat2"},
                new Book {BookId = 3, Title = "b3", Category = "Cat1"},
                new Book {BookId = 4, Title = "b4", Category = "Cat2"},
                new Book {BookId = 5, Title = "b5", Category = "Cat3"}
            }.AsQueryable());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            var result = (controller.Index("Cat2", 1)?.ViewData.Model
                as BooksListViewModel ?? new()).Books.ToArray();

            Assert.Equal(2, result.Length);
            Assert.True(result[0].Title == "b2" && result[0].Category == "Cat2");
            Assert.True(result[1].Title == "b4" && result[1].Category == "Cat2");
        }

        [Fact]
        public void GenerateCategorySpecificBookCount() {
            var mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new Book {BookId = 1, Title = "P1", Category = "Cat1"},
                new Book {BookId = 2, Title = "P2", Category = "Cat2"},
                new Book {BookId = 3, Title = "P3", Category = "Cat1"},
                new Book {BookId = 4, Title = "P4", Category = "Cat2"},
                new Book {BookId = 5, Title = "P5", Category = "Cat3"}
            }.AsQueryable());

            var target = new HomeController(mock.Object);
            target.PageSize = 3;

            Func<ViewResult, BooksListViewModel?> GetModel = result
                => result?.ViewData?.Model as BooksListViewModel;

            int? res1 = GetModel(target.Index("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.Index("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.Index("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.Index(null))?.PagingInfo.TotalItems;

            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}