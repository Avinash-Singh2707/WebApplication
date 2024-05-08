using Assesment1.Models;

namespace Assesment1.Repository
{
    public interface IOrder
    {
        string GetBill(int orderId);
        List<Customer> GetCustomersByOrderDate(DateTime orderDate);

        List<Order> GetAll();

        Customer GetCustomerWithHighestOrder();
        void PlaceOrder(Order order);
    }

}
