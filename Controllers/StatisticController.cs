using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.Services;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class StatisticController : Controller
    {
        PdfService pdfService = new PdfService();
        PdfTemplates pdfTemplates = new PdfTemplates();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _GetStatistics()
        {
            return View();
        }
        [HttpPost]
        // GET: Statistic
        public ActionResult GetStatistics(StatisticViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                pdfService.GetStatistics(viewModel);
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult PrintStatistics(StatisticViewModel viewModel)
        {
            //List<OrderModels> orderlist = new List<OrderModels>();
            //foreach (var order in viewModel.orders)
            //{
            //    orderlist.Add(order);
            //}

            var aViewModel = new StatisticViewModel
            {
                orders = viewModel.orders,
                totalSum = viewModel.totalSum,
                totalHatsCount = viewModel.totalHatsCount,
                totalOrdersCount = viewModel.totalOrdersCount,
                time = viewModel.time
            };

            //foreach(var order in viewModel.orders)
            //{
            //    aViewModel.orders.Add(order);
            //}
            pdfTemplates.StatisticsPDF(aViewModel);
            return View("Index");
        }
    }
}