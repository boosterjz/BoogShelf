using System.Linq;
using BookShelf.Models;
using Xunit;

namespace BookShelf.Tests {

    public class CartTests {

        [Fact]
        public void CanAddNewLines() {

            var b1 = new Book { BookId = 1, Title = "B1" };
            var b2 = new Book { BookId = 2, Title = "B2" };

            var target = new Cart();

            target.AddItem(b1, 1);
            target.AddItem(b2, 1);
            var results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(b1, results[0].Book);
            Assert.Equal(b2, results[1].Book);
        }

        [Fact]
        public void CanAddQuantityForExistingLines() {
            var b1 = new Book { BookId = 1, Title = "B1" };
            var b2 = new Book { BookId = 2, Title = "B2" };

            var target = new Cart();

            target.AddItem(b1, 1);
            target.AddItem(b2, 1);
            target.AddItem(b1, 10);
            var results = (target.Lines ?? new())
                .OrderBy(l => l.Book.BookId).ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line() {
            var b1 = new Book { BookId = 1, Title = "B1" };
            var b2 = new Book { BookId = 2, Title = "B2" };
            var b3 = new Book { BookId = 3, Title = "B3" };

            var target = new Cart();
            target.AddItem(b1, 1);
            target.AddItem(b2, 3);
            target.AddItem(b3, 5);
            target.AddItem(b2, 1);

            target.RemoveLine(b2);

            Assert.Empty(target.Lines.Where(l => l.Book == b2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total() {
            var b1 = new Book { BookId = 1, Title = "B1", Price = 100M };
            var b2 = new Book { BookId = 2, Title = "B2", Price = 50M };


            var target = new Cart();

            target.AddItem(b1, 1);
            target.AddItem(b2, 1);
            target.AddItem(b1, 3);
            var result = target.ComputeTotalValue();

            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clear_Contents() {
            var b1 = new Book { BookId = 1, Title = "B1", Price = 100M };
            var b2 = new Book { BookId = 2, Title = "B2", Price = 50M };

            var target = new Cart();

            target.AddItem(b1, 1);
            target.AddItem(b2, 1);

            target.Clear();

            Assert.Empty(target.Lines);
        }
    }
}