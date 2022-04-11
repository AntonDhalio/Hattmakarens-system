using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Hattmakarens_system.Controllers
{
    public class HatController : Controller
    {
        HatRepository hatRepository = new HatRepository();
        OrderRepository orderRepository = new OrderRepository();
        CustomerRepository customerRepository = new CustomerRepository();
        HatmodelRepository hatModelRepository = new HatmodelRepository();

        // GET: Hat
        public ActionResult Index()
        {
            return View();
        }

        // GET: Hat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hat/Create
        public ActionResult CreateSpec(int orderId, string customerEmail)
        {
            HatViewModel model = new HatViewModel()
            {
                OrderId = orderId,
                CustomerEmail = customerEmail
            };
            return View(model);
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateSpec(HatViewModel model)
        {
            try
            {
                model.HatModelID = 1; //Hårdkodat värde för att representera specialltillverkad hatt
                //hatRepository.CreateHat(model);
                hatRepository.CreateHat(model);
                //orderRepository.OrderAddHat(model);
                //OrderViewModel orderModel = new OrderViewModel();
                //orderModel.Id = model.OrderId;
                //OrderRepository.AddSpecHat(model);
                return RedirectToAction("CreateOrder", "Order", new {currentOrderId = model.OrderId, customerEmail = model.CustomerEmail});
            }
            catch
            {
                return View();
            }
        }
        // GET: Hat/Create
        public ActionResult CreateStored(int orderId, string customerEmail, string hatModelName)
        {
            HatViewModel model = new HatViewModel()
            {
                OrderId = orderId,
                CustomerEmail = customerEmail
            };
            if(hatModelName != null)
            {
                var hatModel = hatModelRepository.GetHatmodelByName(hatModelName);
                model.Price = hatModel.Price;
                //model.Path = hatModel.Path -- lägga till path-property på hatmodel i databas?
                //model.Materials = hatModel.Material;
                //model.HatModelName = 
                model.HatModelName = hatModel.Name;
                model.HatModelID = hatModel.Id;
                model.HatModelDescription = hatModel.Description;
            }
            return View(model);
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateStored(HatViewModel model)
        {
            try
            {
                //model.ModelID = 2; //Hårdkodat värde för att representera icke-specialltillverkad hatt
                //hatRepository.CreateHat(model);
                hatRepository.CreateHat(model);
                //orderRepository.OrderAddHat(model);
                //OrderViewModel orderModel = new OrderViewModel();
                //orderModel.Id = model.OrderId;
                //OrderRepository.AddSpecHat(model);
                return RedirectToAction("CreateOrder", "Order", new { currentOrderId = model.OrderId, customerEmail = model.CustomerEmail });
            }
            catch
            {
                return View();
            }
        }

        // GET: Hat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hat/Edit/5
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

        // GET: Hat/Delete/5
        public ActionResult Delete(int id, int orderId)
        {
            try
            {
                HatViewModel model = hatRepository.GetHatViewModel(id);
                model.OrderId = orderId;
                //var customer = customerRepository.GetCustomerByOrderId(orderId);
                //model.CustomerEmail = customer.Email;
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // POST: Hat/Delete/5
        [HttpPost]
        public ActionResult Delete(HatViewModel model)
        {
            try
            {
                hatRepository.DeleteHat(model.Id);
                var customer = customerRepository.GetCustomerByOrderId(model.OrderId);
                return RedirectToAction("CreateOrder", "Order", new { currentOrderId = model.OrderId, customerEmail = customer.Email});
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ActiveHats()
        {
            var repo = new HatRepository();
            var allHats = new List<Hats>();
            using (var context = new ApplicationDbContext())
            {
                allHats = context.Hats.Include(h => h.Order).ToList();
            }
            var hatstoShow = allHats.Where(h => h.UserId.Equals(User.Identity.GetUserId())).Where(h => h.Status == "Aktiv");
            var viewModel = new ActiveHatsViewModel
            {
                hats = hatstoShow.ToList()
            };
            return View(viewModel);
        }
    }
}
