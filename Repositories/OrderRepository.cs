 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hattmakarens_system.Models;
using Hattmakarens_system.ViewModels;
using System.Web.Mvc;

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
                return hatCon.Order.Include(o => o.Hats).Include(o => o.Customer).
                    Include(o => o.User).ToList();
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

        public void UpdateOrderPrice(int orderId)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                var order = GetOrder(orderId);
                var updatedOrder = CaluculateOrderTotal(order);
                order.Moms = updatedOrder.Moms;
                order.TotalSum = updatedOrder.TotalSum;
                hatCon.Entry(order).State = EntityState.Modified;
                hatCon.SaveChanges();
            }
        }

        public OrderModels CaluculateOrderTotal(OrderModels order)
        {
            var hats = hatRepository.GetAllHatsByOrderId(order.Id);
            var calculate = new Service.Calculate();
            var sumHats = calculate.GetTotalPriceExTax(hats);
            order.TotalSum = calculate.CalculateTax(sumHats, order.Priority);
            order.Moms = calculate.GetTaxFromTotal(order.TotalSum);
            return order;
        }

        public OrderModel CaluculateOrderTotal(OrderModel order, List<HatViewModel> listOfHats)
        {
            List<Hats> hats = new List<Hats>();
            foreach (HatViewModel hatModel in listOfHats)
            {
                Hats hat = new Hats()
                {
                    Price = hatModel.Price,
                    Name = hatModel.Name
                };
                hats.Add(hat);
            };
            var calculate = new Service.Calculate();
            var sumHats = calculate.GetTotalPriceExTax(hats);
            order.TotalSum = calculate.CalculateTax(sumHats, order.Priority);
            order.Moms = calculate.GetTaxFromTotal(order.TotalSum);
            return order;
        }

        public void CreateOrder(OrderModel model, List<HatViewModel> hatViewModels)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                OrderModels finishedOrder = new OrderModels()
                {
                    Date = DateTime.Now,
                    Priority = model.Priority,
                    Status = "Aktiv",
                    Comment = model.Comment,
                    UserId = model.UserId,
                    CustomerId = model.CustomerId,
                    Hats = model.Hats
                };
                var order = CaluculateOrderTotal(model, hatViewModels);
                finishedOrder.TotalSum = order.TotalSum;
                finishedOrder.Moms = order.Moms;
                hatCon.Order.Add(finishedOrder);
                hatCon.SaveChanges();
            };
        }

        public int GetDBLastAddedOrderId()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Order.Max(h => h.Id);
            }
        }
        public OrderModels ChangeCustomer (int id, int customerId)
        {
            using (var context = new ApplicationDbContext())
            {
                var order = context.Order.FirstOrDefault(o => o.Id == id);
                order.CustomerId = customerId;
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
                return order;
            }
        }
    }
}