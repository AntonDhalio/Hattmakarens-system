using Hattmakarens_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Service
{
    public class Costumer
    {
        public CustomerModels GetCustomerInfo(int id)
        {
            try
            {
                var customer = new Repositories.CustomerRepository().GetCustomer(id);
                var orders = new Repositories.CustomerRepository().GetAllCustomerOrders(id);
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

    }
}