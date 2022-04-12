using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hattmakarens_system.Models;
using Hattmakarens_system.ViewModels;

namespace Hattmakarens_system.Repositories
{
    public class OrderRepository
    {
        //private HatRepository hatRepository = new HatRepository();
        //private CustomerRepository customerRepository = new CustomerRepository();

        public OrderModels GetOrder(int? id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Order.Include(o => o.Hats).FirstOrDefault(o => o.Id == id);
            }
        }

        public List<OrderModels> GetAllOrders()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Order.Include(o => o.Hats).Include(o => o.Customer).ToList();
            }
        }
        //public OrderModels SaveOrder(OrderModels order)
        //{
        //    using (var hatCon = new ApplicationDbContext())
        //    {
        //        if (order.Id != 0)
        //        {
        //            hatCon.Entry(order).State = EntityState.Modified;
        //        }
        //        else
        //        {
        //            hatCon.Order.Add(order);
        //        }
        //        hatCon.SaveChanges();
        //        return order;
        //    }
        //}

        public void CreateOrder(OrderModel order)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                OrderModels newOrdermodel = new OrderModels()
                {
                    Date = DateTime.Now,
                    Priority = order.Priority,
                    Status = "Aktiv",
                    Comment = order.Comment
                };
                hatCon.Order.Add(newOrdermodel);
                hatCon.SaveChanges();
            }
        }

        public int CreateEmptyOrderModel(int customerId)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                OrderModels newOrderModel = new OrderModels();
                newOrderModel.Date = DateTime.Now;
                newOrderModel.CustomerId = customerId;
                hatCon.Order.Add(newOrderModel);
                hatCon.SaveChanges();
                return newOrderModel.Id;
            }
        }
        public int CreateOrderInDatabase(int customerId)
        {
            //Kolla vem som är inloggad och koppla som skapare på beställning
            int newOrderId = CreateEmptyOrderModel(customerId);
            return newOrderId;

        }
        public void DeleteOrder(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                var order = hatCon.Order.FirstOrDefault(o => o.Id == id);
                if (order != null)
                {
                    hatCon.Order.Remove(order);
                    hatCon.SaveChanges();
                }
            }
        }

        public void OrderAddHat(HatViewModel model)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                int id = model.OrderId;
                var order = hatCon.Order.FirstOrDefault(o => o.Id == id);
                //Hats hat = hatRepository.CreateHat(model);
                //order.Hats.Add(hat);
                hatCon.SaveChanges();
            }
        }
        //public void AddSpecHat(HatViewModel specHat)
        //{
        //    GetOrder(specHat.OrderId);

        //}
        public OrderModel GetOrderViewModel(int? id, string customerEmail)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            HatRepository hatRepository = new HatRepository();
            var order = GetOrder(id);
            string customerName = customerRepository.GetCustomerNameById(order.CustomerId);
            OrderModel orderViewModel = new OrderModel()
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = customerName,
                CustomerEmail = customerEmail,
                Hats = hatRepository.GetAllHatsByOrderId(order.Id),
                Comment = order.Comment
            };
            return orderViewModel;
        }

        public void UpdateOrder(int? id, string comment, bool priority, string userId)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                var order = GetOrder(id);
                order.Comment = comment;
                order.Priority = priority;
                order.Status = "Aktiv";
                order.UserId = userId;
                hatCon.Entry(order).State = EntityState.Modified;
                hatCon.SaveChanges();
            }
        }
    }
}