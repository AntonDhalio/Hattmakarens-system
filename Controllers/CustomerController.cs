using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
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
            try
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
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult ChangeCustomer(int id)
        {
            var showCustomerInfo = new Service.Costumer().GetCustomerInfo(id);
            return View(showCustomerInfo);
        }
        [HttpPost]
        public ActionResult ChangeCustomer(CostumerViewModel model)
        {
            
            var status = new Service.Costumer().EditCustomerInfo(model);
            if(status == true)
            {
                return RedirectToAction("DisplayCustomer", new {id = model.Id});
            }
            else
            {
                return View();
            }

                
        }

        public ActionResult DisplayCustomer(int id)
        {
            var showCustomerInfo = new Service.Costumer().GetCustomerInfo(id);
            return View(showCustomerInfo);
        }

        public ActionResult DeleteCustomer(int id)
        {
            if(id != 0)
            {
                new CustomerRepository().DeleteCostumer(id);
                return RedirectToAction("Index", "Home");
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