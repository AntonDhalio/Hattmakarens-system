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
        HatRepository hatRepository = new HatRepository();
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

        public int CreateEmptyOrderModel(int customerId, string userId)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                OrderModels newOrderModel = new OrderModels();
                newOrderModel.Date = DateTime.Now;
                newOrderModel.CustomerId = customerId;
                newOrderModel.UserId = userId;
                newOrderModel.Status = "Aktiv";
                hatCon.Order.Add(newOrderModel);
                hatCon.SaveChanges();
                return newOrderModel.Id;
            }
        }
        public int CreateOrderInDatabase(int customerId, string userId)
        {
            int newOrderId = CreateEmptyOrderModel(customerId, userId);
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
                hatCon.SaveChanges();
            }
        }
        public OrderModel GetOrderViewModel(int? id, string customerEmail)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            HatRepository hatRepository = new HatRepository();
            var order = GetOrder(id);
            string customerName = customerRepository.GetCustomerNameById(order.CustomerId);
            OrderModel orderViewModel = new OrderModel()
            {
                Id = order.Id,
                Priority = order.Priority,
                Status = order.Status,
                CustomerId = order.CustomerId,
                CustomerName = customerName,
                CustomerEmail = customerEmail,
                Hats = hatRepository.GetAllHatsByOrderId(order.Id),
                Comment = order.Comment,
                TotalSum = order.TotalSum,
                Moms = order.Moms
            };
            return orderViewModel;
        }

        public void UpdateOrder(OrderModel model)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                var order = GetOrder(model.Id);
                order.Comment = model.Comment;
                order.Priority = model.Priority;
                var updatedOrder = CaluculateOrderTotal(model);
                order.Moms = updatedOrder.Moms;
                order.TotalSum = updatedOrder.TotalSum;
                hatCon.Entry(order).State = EntityState.Modified;
                hatCon.SaveChanges();
            }
        }

        public OrderModel CaluculateOrderTotal(OrderModel order)
        {
            var hats = hatRepository.GetAllHatsByOrderId(order.Id);
            var calculate = new Service.Calculate();
            var sumHats = calculate.GetTotalPriceExTax(hats);
            order.TotalSum = calculate.CalculateTax(sumHats, order.Priority);
            order.Moms = calculate.GetTaxFromTotal(order.TotalSum);
            return order;
        }
    }
}