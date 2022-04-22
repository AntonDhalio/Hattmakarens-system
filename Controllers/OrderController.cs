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
        UserRepository userRepository = new UserRepository();

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
        public ActionResult CreateOrder(string customerEmail)
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
            if (customerEmail != null && TempData.Peek("hat") == null)
            {
                OrderModel orderModel = (OrderModel)TempData.Peek("order");
                orderModel.UserId = User.Identity.GetUserId();
                orderModel.CustomerName = customerRepository.GetCustomerByEmail(customerEmail).Name;
                orderModel.CustomerEmail = customerEmail;
                orderModel.CustomerId = customerRepository.GetCustomerByEmail(customerEmail).Id;
                TempData["order"] = orderModel;
                TempData.Keep("order");

                return View(orderModel);
            }
            else
            {
                List<HatViewModel> hatModels = new List<HatViewModel>();
                hatModels = (List<HatViewModel>)TempData.Peek("listOfHats");
                HatViewModel hat = (HatViewModel)TempData["hat"];
                if (hatModels.Count == 0)
                {
                    hat.Id = 1;
                }
                else
                {
                    hat.Id = hatModels.Max(h => h.Id) + 1;
                }
                hatModels.Add(hat);
                OrderModel order = orderRepository.CaluculateOrderTotal((OrderModel)TempData.Peek("order"), (List<HatViewModel>)TempData.Peek("listOfHats"));
                return View(order);
            }
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult CreateOrder(OrderModel model)
        {
            try 
            {
                var listOfHats = (List<HatViewModel>)TempData.Peek("listOfHats");
                if (listOfHats.Count > 0)
                {
                    TempData["message"] = "";
                    OrderModel order = (OrderModel)TempData["order"];
                    order.Comment = model.Comment;
                    order.Priority = model.Priority;
                    order.TotalSum = model.TotalSum;
                    order.Moms = model.Moms;
                    
                    orderRepository.CreateOrder(order, (List<HatViewModel>)TempData["listOfHats"]);
                    var orderId = orderRepository.GetDBLastAddedOrderId();
                    List<HatViewModel> hats = (List<HatViewModel>)TempData.Peek("listOfHats");
                    foreach (var hat in hats)
                    {
                        hatRepository.CreateHat(hat, orderId);
                    }

                    return RedirectToAction("ActiveHats", "Hat");
                }
                else
                {
                    TempData["message"] = "Order måste innehålla minst en hatt";
                    var order = (OrderModel)TempData.Peek("order");
                    return RedirectToAction("CreateOrder", "Order", new { customerEmail = order.CustomerEmail });
                }
                
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
            ViewBag.Users = userRepository.DictionaryUsers();
            return View(order);
        }

        public ActionResult ModifyOrder(int Id)
        {
            var customer = customerRepository.GetCustomerByOrderId(Id);
            OrderModel order = orderRepository.GetOrderViewModel(Id, customer.Email);
            ViewBag.Users = userRepository.DictionaryUsers();
            return View(order);
        }

        [HttpPost]
        public ActionResult ModifyOrder(int id, string comment, string orderStatus)
        {
            new Service.Order().ChangeOrderStatus(id, orderStatus);
            new Service.Order().ChangeOrderComment(id, comment);
            orderRepository.UpdateOrderPrice(id);
            return RedirectToAction("ViewOrder", new {Id = id});
        }

        public ActionResult ChangePriority(int id, bool status)
        {
            new Service.Order().ChangePriorityStatus(id, status);
            return RedirectToAction("ModifyOrder", new {id = id});
        }
    }
}
