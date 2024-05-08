using Assesment1.Models;
using Assesment1.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Assesment1.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _order;

        public OrderController(IOrder repository)
        {
            _order = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        //https://localhost:44353/Order/CustomerDetailsByOrderDate?orderDate=1996-07-04
        public IActionResult CustomerDetailsByOrderDate(DateTime orderDate)
        {
            var customers = _order.GetCustomersByOrderDate(orderDate);
            return View(customers);
        }

        //https://localhost:44353/order/displaybill?orderid=10284
        public IActionResult DisplayBill(int orderId)
        {
            var bill = _order.GetBill(orderId);
            return Content(bill);
        }

        public IActionResult AllOrders()
        {
            var orders = _order.GetAll();
            return View(orders);
        }

        //https://localhost:44353/order/HighestOrderCustomer
        public IActionResult HighestOrderCustomer()
        {
            var highestOrderCustomer = _order.GetCustomerWithHighestOrder();
            return View(highestOrderCustomer);
        }

        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            _order.PlaceOrder(order);
            return RedirectToAction("AllOrders");
        }
    }
}
