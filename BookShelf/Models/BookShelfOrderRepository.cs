using Microsoft.EntityFrameworkCore;

namespace BookShelf.Models {

    public class EFOrderRepository : IOrderRepository {
        private BookShelfDbContext context;

        public EFOrderRepository(BookShelfDbContext ctx) {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Book);

        public void SaveOrder(Order order) {
            context.AttachRange(order.Lines.Select(l => l.Book));
            if (order.OrderID == 0) {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}