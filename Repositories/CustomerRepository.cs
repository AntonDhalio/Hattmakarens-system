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
        public List<OrderModels> GetAllCustomerOrders(int id)
        {
            var customerOrders = new List<OrderModels>();
            using (var hatCon = new ApplicationDbContext())
            {
                var orders = new OrderRepository().GetAllOrders();
                foreach (var item in orders)
                {
                    if(item.CustomerId == id)
                    {
                        customerOrders.Add(item);
                    }
                }
            }
            return customerOrders;
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

        public List<CustomerModels> GetAllCustomersByName(string name)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return (hatCon.Customer.Where(r => r.Name.Contains(name)).ToList());
            }
        }

        public CustomerModels GetCustomerByEmail(string email)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Customer.FirstOrDefault(c => c.Email == email);
            }

        }
        public int GetCustomerIdByEmail(string email)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                CustomerModels customer = hatCon.Customer.FirstOrDefault(c => c.Email == email);
                int customerId = customer.Id;
                return customerId;
            }
        }

        public string GetCustomerNameById(int Id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                CustomerModels customer = hatCon.Customer.FirstOrDefault(c => c.Id == Id);
                string customerName = customer.Name;
                return customerName;
            }
        }

        public CustomerModels GetCustomerByOrderId(int? orderId)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                OrderRepository orderRepository = new OrderRepository();
                var order = orderRepository.GetOrder(orderId);
                return GetCustomer(order.CustomerId);
            }
        }

        public CustomerModels AddEmptyCustomer()
        {
            using (var context = new ApplicationDbContext())
            {
                var customer = new CustomerModels
                {
                    Name = "Kund borttagen",
                    Adress = "",
                    Phone = 0,
                    Comment = "När du tar bort en kund kommer alla dess tidigare ordrar att hamna på denna 'kund'",
                    Email = "",
                    Orders = new List<OrderModels>()
                };
                context.Customer.Add(customer);
                context.SaveChanges();
                return customer;
            }
        }

    }
}