using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Services
{
    public class PdfService
    {
        OrderRepository orderRepository = new OrderRepository();
        CustomerRepository customerRepository = new CustomerRepository();
        HatRepository hatRepository = new HatRepository();
        PdfTemplates PdfTemplates = new PdfTemplates();

        public void PrintInvoice(InvoiceViewModel model, int id)
        {
            var order = orderRepository.GetOrder(id);
            var customer = customerRepository.GetCustomer(order.CustomerId);
            model.Customer = customer;
            model.Order = order;
            PdfTemplates.InvoicePDF(model);
        }

        public void PrintShipping(ShippingViewModel model, int id)
        {
            var order = orderRepository.GetOrder(id);
            var customer = customerRepository.GetCustomer(order.CustomerId);
            model.Customer = customer;
            model.Order = order;
            PdfTemplates.ShippingPDF(model);
        }

        public StatisticViewModel GetStatistics(StatisticViewModel viewModel)
        {
            var repo = new OrderRepository();
            var orders = repo.GetAllOrders();
            viewModel.orders = new List<Models.OrderModels>();
            foreach (var order in orders)
            {
                if(order.Date >= viewModel.fromDate && order.Date <= viewModel.toDate || order.CustomerId.Equals(viewModel.customerId.Value))
                {
                    viewModel.orders.Add(order);
                }
            }
            viewModel.totalOrdersCount = viewModel.orders.Count;
            var hats = 0;
            foreach (var order in viewModel.orders)
            {
                hats += order.Hats.Count;
            }
            viewModel.totalHatsCount = hats;
            var sum = 0.0;
            foreach (var order in viewModel.orders)
            {
                sum += order.TotalSum;
            }
            viewModel.totalSum = sum;

            return viewModel;
        }
    }
}