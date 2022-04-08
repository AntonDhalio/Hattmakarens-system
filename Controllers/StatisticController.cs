using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _GetStatistics()
        {
            var orders = new List<OrderModels>();
            using (var context = new ApplicationDbContext())
            {
                foreach(var order in context.Order)
                {
                    orders.Add(order);
                }
            }
            ViewBag.Orders = orders;
            return View();
        }
        // GET: Statistic
        public ActionResult GetStatistics(StatisticViewModel viewModel)
        {
            var year = DateTime.Now.AddYears(-1);
            var quarter = DateTime.Now.AddMonths(-3);
            var month = DateTime.Now.AddMonths(-1);
            var orders = viewModel.orders;
            if(viewModel.time.Equals("År"))
            {
                //viewModel.orders = (List<Models.OrderModels>)viewModel.orders.Where(o => o.Date < year);
                viewModel.orders = new List<Models.OrderModels>();
                foreach (var order in orders)
                {
                    if(order.Date < year)
                    {
                        viewModel.orders.Add(order);
                    }
                }
            }
            else if (viewModel.time.Equals("Kvartal"))
            {
                viewModel.orders = (List<Models.OrderModels>)viewModel.orders.Where(o => o.Date < quarter);
            }
            else if (viewModel.time.Equals("Månad"))
            {
                viewModel.orders = (List<Models.OrderModels>)viewModel.orders.Where(o => o.Date < month);
            }
            viewModel.totalOrdersCount = viewModel.orders.Count;
            var hats = 0;
            foreach (var order in viewModel.orders)
            {
                hats += order.Hats.Count;
            }
            viewModel.totalHatsCount = hats;
            var sum = 0.0;
            foreach(var order in viewModel.orders)
            {
                sum += order.TotalSum;
            }
            return View(viewModel);
        }
    }
}