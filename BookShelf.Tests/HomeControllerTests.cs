using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BookShelf.Controllers;
using BookShelf.Models;
using Moq;
using Xunit;

namespace BookShelf.Tests {
    public class HomeControllerTest {
        [Fact]
        public void CanUseRepository() {
            // устанавливаем имитацию базы данных для контроллера
            var mock = new Mock<IRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new Book { BookId = 1, Title = "Book1" },
                new Book { BookId = 2, Title = "Book2" },
            }.AsQueryable<Book>());
            // создаем коонтроллер
            var controller = new HomeController(null, mock.Object);

            // получаем данные, переданные в контроллер
            var result = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Book>;

            // делаем проверки
            var resultArray = result.ToArray();
            Assert.Equal(2, resultArray.Length);
            Assert.Equal("Book1", resultArray[0].Title);
            Assert.Equal("Book2", resultArray[1].Title);
        }
    }
}