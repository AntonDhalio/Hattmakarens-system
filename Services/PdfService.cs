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
            viewModel.orders = new List<Models.OrderModels>();
            var orders = repo.GetAllOrders();

            foreach (var order in orders)
            {
                bool hatmodelExist = false;
                foreach (var hat in order.Hats)
                {
                    if (viewModel.hatmodelId != null && hat.ModelID.ToString().Equals(viewModel.hatmodelId))
                    {
                        hatmodelExist = true;
                    }
                }

                if (order.Date >= viewModel.fromDate && order.Date <= viewModel.toDate)
                {
                    viewModel.orders.Add(order);
                }
                else if (viewModel.customerId != null && order.CustomerId.ToString().Equals(viewModel.customerId))
                {
                    viewModel.orders.Add(order);
                } 
                else if(hatmodelExist)
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