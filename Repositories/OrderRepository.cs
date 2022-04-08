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
        public OrderModels GetOrder(int? id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Order.FirstOrDefault(o => o.Id == id);
            }
        }

        public List<OrderModels> GetAllOrders()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Order.ToList();
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

        public void CreateOrder(OrderViewModel order)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                OrderModels newOrdermodel = new OrderModels()
                {
                    Date = DateTime.Now,
                    Priority = order.Priority,
                    Status = "Under behandling",
                    Comment = order.Comment
                };
                hatCon.Order.Add(newOrdermodel);
                hatCon.SaveChanges();
            }
        }

        public int CreateEmptyOrder(int customerId)
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

        //public void AddSpecHat(HatViewModel specHat)
        //{
        //    GetOrder(specHat.OrderId);

        //}
    }
}