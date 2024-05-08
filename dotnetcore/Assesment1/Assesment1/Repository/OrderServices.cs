using Assesment1.Models;
using Microsoft.EntityFrameworkCore;

namespace Assesment1.Repository
{
    public class OrderServices : IOrder
    {
        private readonly NorthwindContext _context;
        public OrderServices(NorthwindContext context)
        {

            _context = context;
        }
        public string GetBill(int oId)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == oId);

            if (order == null)
            {
                return "Order not found.";
            }

            decimal totalBill = 0;
            foreach (var orderDetail in order.OrderDetails)
            {
                totalBill += orderDetail.Quantity * orderDetail.UnitPrice;
            }

            return $"Total bill for Order ID {oId}: Rs. {totalBill}";
        }


        public List<Customer> GetCustomersByOrderDate(DateTime orderDate)
        {
            return _context.Customers.Where(c => c.Orders.Any(o => o.OrderDate == orderDate)).ToList();
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Customer GetCustomerWithHighestOrder()
        {
            return _context.Orders
                .GroupBy(o => o.CustomerId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.First().Customer)
                .FirstOrDefault();
        }

        public void PlaceOrder(Order order)
        {
            _context.Add(order);
        }
    }
}

