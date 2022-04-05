using System;
using System.Collections.Generic;
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
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult ChangeCustomer()
        {
            int id = 1;
            var showCustomerInfo = new Service.Costumer().GetCustomerInfo(id);
            return View(showCustomerInfo);
        }
        [HttpPost]
        //public ActionResult ChangeCustomer()
        //{

        //}
        
        public ActionResult DisplayCustomer()
        {
            int id = 1;
            var showCustomerInfo = new Service.Costumer().GetCustomerInfo(id);
            return View(showCustomerInfo);
        }
    }
}