using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.Service;
using Hattmakarens_system.Services;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Hattmakarens_system.Controllers
{
    public class StatisticController : Controller
    {
        PdfService pdfService = new PdfService();
        PdfTemplates pdfTemplates = new PdfTemplates();
        public ActionResult Index()
        {
            var viewModel = new StatisticViewModel();
            viewModel.customers = StatisticCustomers();
            viewModel.hatmodels = StatisticHatModels();
            return View(viewModel);
        }

        public ActionResult _GetStatistics(StatisticViewModel viewModel)
        {
            viewModel.customers = StatisticCustomers();
            viewModel.hatmodels = StatisticHatModels();
            ViewBag.Customers = StatisticCustomers();
            return View(viewModel);
        }
        [HttpPost]
        // GET: Statistic
        public ActionResult GetStatistics(StatisticViewModel viewModel)
        {

            viewModel.customers = StatisticCustomers();
            viewModel.hatmodels = StatisticHatModels();
            viewModel.customerId = Request.Form["customerId"];
            viewModel.hatmodelId = Request.Form["hatmodelId"];
            if (ModelState.IsValid)
            {
                pdfService.GetStatistics(viewModel);
                return View(viewModel);
            }
            else
            {
                return View("Index", viewModel);
            }
        }
        [HttpPost]
        public ActionResult PrintStatistics(StatisticViewModel viewModel)
        {
            var aViewModel = new StatisticViewModel
            {
                orders = viewModel.orders,
                totalSum = viewModel.totalSum,
                totalHatsCount = viewModel.totalHatsCount,
                totalOrdersCount = viewModel.totalOrdersCount,
                time = viewModel.time,
                fromDate = viewModel.fromDate,
                toDate = viewModel.toDate
            };

            pdfTemplates.StatisticsPDF(aViewModel);
            return new HttpStatusCodeResult(204);
        }
        [HttpPost]
        public ActionResult CountTax(StatisticViewModel viewModel)
        {
            var count = new Calculate();
            var taxFromOrdersCount = count.GetTaxFromTotal(viewModel.totalSum);
            var inTax = viewModel.purchasedTax;
            var result = taxFromOrdersCount - inTax;
            string uploadPath = Server.MapPath("~/xmlFile");
            var xml = new XmlService();
            xml.TaxXml(inTax, taxFromOrdersCount, result, uploadPath);
            return new HttpStatusCodeResult(204);
        }
        public List<SelectListItem> StatisticCustomers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var custRepo = new CustomerRepository();
            foreach(var customer in custRepo.GetAllCostumers())
            {
                if(!customer.Name.Equals("Kund borttagen"))
                {
                    list.Add(new SelectListItem
                    {
                        Value = customer.Id.ToString(),
                        Text = customer.Name
                    });
                }
                
            }
            return list;
        }
        public List<SelectListItem> StatisticHatModels()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var modelRepo = new HatmodelRepository();
            foreach (var hatmodel in modelRepo.GetAllHatmodels())
            {
                list.Add(new SelectListItem
                {
                    Value = hatmodel.Id.ToString(),
                    Text = hatmodel.Name
                });
            }
            return list;
        }
    }
}