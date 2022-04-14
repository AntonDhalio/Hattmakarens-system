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
        public ActionResult CreateOrder(string customerEmail/*, int? currentOrderId*/)
        {
            
            if (customerEmail == null)
            {
                OrderModel order = new OrderModel();
                order.TotalSum = 0;
                order.Moms = 0;

                TempData["listOfHats"] = new List<HatViewModel>();
                TempData["hat"] = null;
                TempData["order"] = order;
                TempData.Keep("order");
                
                return View(order);
            }
            if (customerEmail != null && TempData.Peek("hat") == null /* && currentOrderId == null*/)
            {
                OrderModel orderModel = (OrderModel)TempData.Peek("order");
                orderModel.UserId = User.Identity.GetUserId();
                orderModel.CustomerEmail = customerEmail;
                orderModel.CustomerId = customerRepository.GetCustomerIdByEmail(customerEmail);
                TempData["order"] = orderModel;
                TempData.Keep("order");
                
                return View(orderModel);

                //string userId = User.Identity.GetUserId();
                //int customerId = customerRepository.GetCustomerIdByEmail(customerEmail);

                //int orderId = orderRepository.CreateOrderInDatabase(customerId, userId);
                //OrderModel order = orderRepository.GetOrderViewModel(orderId, customerEmail);

                //return View(order);

            }
            if(customerEmail != null && (HatViewModel)TempData.Peek("hat") != null/*&& currentOrderId != null*/)
            {
                List<HatViewModel> hatModels = new List<HatViewModel>();
                hatModels = (List<HatViewModel>)TempData.Peek("listOfHats");
                hatModels.Add((HatViewModel)TempData.Peek("hat"));
                //TempData["listOfHats"] = hatModels;
                //TempData.Keep("listOfHats");
                OrderModel order = orderRepository.CaluculateOrderTotal((OrderModel)TempData.Peek("order"), (List<HatViewModel>)TempData.Peek("listOfHats"));
                return View(order);

                
                //OrderModel order = orderRepository.GetOrderViewModel(currentOrderId, customerEmail);
                //var updatedOrder = orderRepository.CaluculateOrderTotal(order);

                //return View(updatedOrder);
            }
            return View();

        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult CreateOrder(OrderModel model)
        {
            try 
            {
                //orderRepository.UpdateOrder(model);
                OrderModel order = (OrderModel)TempData["order"];
                order.Comment = model.Comment;
                order.Priority = model.Priority;
                order.TotalSum = model.TotalSum;
                order.Moms = model.Moms;
                orderRepository.CreateOrder(order, (List<HatViewModel>)TempData["listOfHats"]);
                var orderId = orderRepository.GetDBLastAddedOrderId();
                List<HatViewModel> hats = (List<HatViewModel>)TempData.Peek("listOfHats");
                foreach(var hat in hats)
                {
                    hatRepository.CreateHat(hat, orderId);
                }

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
    }
}
