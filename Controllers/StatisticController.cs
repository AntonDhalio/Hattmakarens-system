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
            return View("Index");
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
    }
}