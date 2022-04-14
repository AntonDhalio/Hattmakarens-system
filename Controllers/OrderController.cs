using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using Microsoft.AspNet.Identity;
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
                OrderModel order = new OrderModel();
                order.TotalSum = 0;
                order.Moms = 0;
                return View(order);
            }
            if (customerEmail != null && currentOrderId == null)
            {
                string userId = User.Identity.GetUserId();
                int customerId = customerRepository.GetCustomerIdByEmail(customerEmail);
                int orderId = orderRepository.CreateOrderInDatabase(customerId, userId);
                OrderModel order = orderRepository.GetOrderViewModel(orderId, customerEmail);

                return View(order);

            }
            if(customerEmail != null && currentOrderId != null)
            {
                OrderModel order = orderRepository.GetOrderViewModel(currentOrderId, customerEmail);
                var updatedOrder = orderRepository.CaluculateOrderTotal(order);

                return View(updatedOrder);
            }
            return View();

        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult CreateOrderAdd(OrderModel model)
        {
            try 
            {           
                orderRepository.UpdateOrder(model);
                return RedirectToAction("ActiveHats", "Hat");
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
            OrderModels order = orderRepository.GetOrder(id);
            OrderModel model = new OrderModel()
            {
                Id = order.Id
            };
            return View(model);
          
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                orderRepository.DeleteOrder(id);
                var hats = hatRepository.GetAllHatsByOrderId(id);
                foreach(var item in hats)
                {
                    hatRepository.DeleteHat(item.Id);
                }
                return RedirectToAction("ActiveHats", "Hat");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult ViewOrder(int Id)
        {
            var customer = customerRepository.GetCustomerByOrderId(Id);
            OrderModel order = orderRepository.GetOrderViewModel(Id, customer.Email);
            return View(order);
        }

        public ActionResult ModifyOrder(int Id)
        {
            var customer = customerRepository.GetCustomerByOrderId(Id);
            OrderModel order = orderRepository.GetOrderViewModel(Id, customer.Email);
            return View(order);
        }

        [HttpPost]
        public ActionResult ModifyOrder(int id, string comment, string orderStatus)
        {
            new Service.Order().ChangeOrderStatus(id, orderStatus);
            new Service.Order().ChangeOrderComment(id, comment);
            return RedirectToAction("ViewOrder", new {Id = id});
        }

        public ActionResult ChangePriority(int id, bool status)
        {
            new Service.Order().ChangePriorityStatus(id, status);
            return RedirectToAction("ModifyOrder", new {id = id});
        }
    }
}
