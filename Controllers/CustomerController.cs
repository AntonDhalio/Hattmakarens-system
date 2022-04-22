using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.Service;
using Hattmakarens_system.ViewModels;

namespace Hattmakarens_system.Controllers
{
    public class CustomerController : Controller
    {
        CustomerRepository customerRepository = new CustomerRepository();
        // GET: Customer
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(CostumerViewModel customerViewModel)
        {
            //try
            //{
            if (customerRepository.ExistingCustomerEmail(customerViewModel.Email) == false)
            {
                if (ModelState.IsValid)
                {
                    var cusRepo = new CustomerRepository();
                    var customer = new CustomerModels
                    {
                        Adress = customerViewModel.Adress,
                        Name = customerViewModel.Name,
                        Email = customerViewModel.Email,
                        Comment = customerViewModel.Comment,
                        Phone = customerViewModel.Phone
                    };
                    cusRepo.SaveCostumer(customer);
                    ModelState.Clear();
                    return RedirectToAction("SearchCustomer", "Customer");
                }
                else
                {
                    return View(customerViewModel);
                }
            }
            else
            {
                ViewBag.Message = "Det finns redan en kund med denna E-post!";
                return View(customerViewModel);
            }
        //}
            //catch
            //{
            //    return View("Error");
            //}
        }
        public ActionResult ChangeCustomer(int id)
        {
            var showCustomerInfo = new Service.Costumer().GetCustomerInfo(id);
            return View(showCustomerInfo);
        }
        [HttpPost]
        public ActionResult ChangeCustomer(CostumerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var status = new Service.Costumer().EditCustomerInfo(model);
                if(status == true)
                {
                    return RedirectToAction("DisplayCustomer", new {id = model.Id});
                }
                return View();
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult DisplayCustomer(int id)
        {
            var showCustomerInfo = new Service.Costumer().GetCustomerInfo(id);
            return View(showCustomerInfo);
        }

        public ActionResult DeleteCustomer(int id)
        {
            var customerService = new Costumer();
            if(id != 0)
            {
                bool activeOrders = false;
                foreach(var order in customerService.GetCustomerInfo(id).Orders)
                {
                    if (order.Status.Equals("Aktiv"))
                    {
                        activeOrders = true;
                    }
                }
                if(activeOrders)
                {
                    MessageBox.Show("Du kan inte ta bort en kund som har aktiva ordrar, vänligen slutför ordrarna först.");
                    return new HttpStatusCodeResult(204);
                } else
                {
                    bool emptyExist = false;
                    var repos = new CustomerRepository();
                    var orderRepo = new OrderRepository();
                    foreach (var customer in repos.GetAllCostumers())
                    {
                        if(customer.Name.Equals("Kund borttagen"))
                        {
                            emptyExist = true;
                        }
                    }
                    if(emptyExist == false)
                    {
                        repos.AddEmptyCustomer();
                    }
                    var emptyCustomer = repos.GetAllCustomersByName("Kund borttagen").First();
                    var ordersToChange = repos.GetAllCustomerOrders(id);
                    foreach(var order in ordersToChange)
                    {
                        order.CustomerId = emptyCustomer.Id;
                        order.Customer = emptyCustomer;
                        orderRepo.ChangeCustomer(order.Id, emptyCustomer.Id);
                    }
                    new CustomerRepository().DeleteCostumer(id);
                    return RedirectToAction("Index", "Home");
                }
                
            }
            else
            {
                Debug.WriteLine("Något har gått fel med id´t");
                return View();
            }
            
        }

        public ActionResult SearchCustomer(string searchString)
        {
            var customers = customerRepository.GetAllCostumers();
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customerRepository.GetAllCustomersByName(searchString).ToList();
            }
            return View(customers);
        }
    }
}