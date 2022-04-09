using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class OrderController : Controller
    {
        CustomerRepository customerRepository = new CustomerRepository();
        OrderRepository orderRepository = new OrderRepository();
        HatRepository hatRepository = new HatRepository();

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult CreateOrder(string customerEmail, int? currentOrderId)
        {
            if (customerEmail == null)
            {
                OrderViewModel order = new OrderViewModel();
                return View(order);
            }
            if (customerEmail != null && currentOrderId == null)
            {
                int customerId = customerRepository.GetCustomerIdByEmail(customerEmail);
                //OrderViewModel order = CreateOrderViewModelWithCustomer(customerEmail);
                int orderId = orderRepository.CreateOrderInDatabase(customerId);
                OrderViewModel order = orderRepository.GetOrderViewModel(orderId, customerEmail);
                //order.CustomerEmail = customerEmail;
                return View(order);
                //    int customerId = customerRepository.GetCustomerIdByEmail(email);
                //    OrderViewModel order = SelectedCustomerEmail(email);
                //    order.Id = Create(customerId);
                //    return View(order);
            }
            if(customerEmail != null && currentOrderId != null)
            {
                OrderViewModel order = orderRepository.GetOrderViewModel(currentOrderId, customerEmail);
                return View(order);
            }
            //if(orderId != null)
            //{
            //    //OrderModels existOrder = orderRepository.GetOrder(orderId);
            //    //OrderViewModel order = new OrderViewModel()
            //    //{
            //    //    Id = existOrder.Id,
            //    //    Hats = existOrder.Hats
            //    //};
            //    return View(orderRepository.GetOrderViewModel(orderId));
            //}
            return View();

        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult CreateOrder(int id/*OrderViewModel orderModel, HatViewModel hatModel */)
        {
            try 
            {
            //    orderRepository.CreateOrder(orderModel);
            //    hatRepository.CreateHat(hatModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public OrderViewModel CreateOrderViewModelWithCustomer(string email)
        {
            CustomerModels customer = customerRepository.GetCustomerByEmail(email);
            var model = new OrderViewModel()
            {
                CustomerId = customer.Id,
                CustomerName = customer.Name
            };
            return model;
        }
    }
}
