namespace BookShelf.Models {
    public class Cart {
        public List<CartLine> Lines { get; set; } = new();

        public virtual void AddItem(Book book, int quantity) {
            var line = Lines.FirstOrDefault(l => l.Book.BookId == book.BookId);

            if (line == null) {
                Lines.Add(new CartLine { Book = book, Quantity = quantity });
            } else {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Book book) =>
            Lines.RemoveAll(l => l.Book.BookId == book.BookId);

        public decimal ComputeTotalValue() =>
            Lines.Sum(l => l.Book.Price * l.Quantity);

        public virtual void Clear() => Lines.Clear();
    }
}