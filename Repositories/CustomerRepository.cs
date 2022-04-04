using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.Repositories
{
    public class CustomerRepository
    {
        public CustomerModels GetCustomer(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Customer.FirstOrDefault(c => c.Id == id);
            }
        }
        public List<CustomerModels> GetAllCostumers()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Customer.ToList();
            }
        }
        public CustomerModels SaveCostumer(CustomerModels costumer)
        {
                using (var hatCon = new ApplicationDbContext())
                {
                    if (costumer.Id != 0)
                    {
                        hatCon.Entry(costumer).State = EntityState.Modified;
                    }
                    else
                    {
                        hatCon.Customer.Add(costumer);
                    }
                    hatCon.SaveChanges();
                    return costumer;
                }
        }
        public void DeleteCostumer(int id)
        {
            using(var hatCon = new ApplicationDbContext())
            {
                var customer = hatCon.Customer.FirstOrDefault(c => c.Id ==id);
                if(customer != null)
                {
                    hatCon.Customer.Remove(customer);
                    hatCon.SaveChanges();
                }
            }
        }
    }
}