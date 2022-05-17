using Microsoft.EntityFrameworkCore;

namespace BookShelf.Models {

    public class EFOrderRepository : IOrderRepository {
        private BookShelfDbContext _context;

        public EFOrderRepository(BookShelfDbContext ctx) {
            _context = ctx;
        }

        public IQueryable<Order> Orders => _context.Orders
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Book);

        public void SaveOrder(Order order) {
            _context.AttachRange(order.Lines.Select(l => l.Book));
            if (order.OrderID == 0) {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
    }
}