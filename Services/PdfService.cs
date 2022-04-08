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
    }
}