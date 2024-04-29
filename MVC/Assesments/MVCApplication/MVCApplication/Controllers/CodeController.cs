using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVCApplication.Models;

namespace MVCApplication.Controllers
{
    public class CodeController : Controller
    {
        // GET: Code
        static northwindEntities db = new northwindEntities();  
        public ActionResult Index()
        {
            return View();
        }
        //Question 1
        public ActionResult GermanyCustomers()
        {
            var CustInGer = db.Customers.Where(c => c.Country == "Germany").ToList();
            return View(CustInGer);
        }

        //Question 2
        public ActionResult CustomerOrderID()
        {
            var CustomerID = db.Customers.Where(c => c.Orders.Any(o => o.OrderID == 10248)).FirstOrDefault();
            if (CustomerID == null)
            {
                return HttpNotFound();
            }
            return View(CustomerID);
        }
    }

}
