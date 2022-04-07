using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Service
{
    public class Costumer
    {
        private CustomerRepository customerRepository = new CustomerRepository();
        public CustomerModels GetCustomerInfo(int id)
        {
            try
            {
                var customer = customerRepository.GetCustomer(id);
                var orders = customerRepository.GetAllCustomerOrders(id);
                var showCustomerInfo = new CustomerModels
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Adress = customer.Adress,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    Comment = customer.Comment,
                    Orders = orders
                };
                return showCustomerInfo;
            }
            catch
            {
                return null;
            }
            
        }

        public bool EditCustomerInfo(CostumerViewModel model)
        {
            try
            {
                var updatedCustomer = new CustomerModels()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Adress = model.Adress,
                    Phone = model.Phone,
                    Email = model.Email,
                    Comment = model.Comment
                };
                customerRepository.SaveCostumer(updatedCustomer);
                return true;

            }
            catch
            {
                return false;
            }
        }

    }
}