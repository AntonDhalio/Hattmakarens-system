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

            //Skapar xml dokument 
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
            XmlElement element1 = doc.CreateElement(string.Empty, "Mainbody", string.Empty);
            doc.AppendChild(element1);
            XmlElement element2 = doc.CreateElement(string.Empty, "title", string.Empty);
            XmlElement element3 = doc.CreateElement(string.Empty, "purchasedTax", string.Empty);
            XmlElement element4 = doc.CreateElement(string.Empty, "salesTax", string.Empty);
            XmlElement element5 = doc.CreateElement(string.Empty, "outgoingTax", string.Empty);
            XmlText purchased = doc.CreateTextNode(inTax.ToString());
            XmlText taxFromOrders = doc.CreateTextNode(taxFromOrdersCount.ToString());
            XmlText subResult= doc.CreateTextNode(result.ToString());
            element1.AppendChild(element2);
            element1.AppendChild(element3);
            element1.AppendChild(element4);
            element1.AppendChild(element5);
            element2.AppendChild(doc.CreateTextNode("Moms"));
            element3.AppendChild(purchased);
            element4.AppendChild(taxFromOrders);
            element5.AppendChild(subResult);
            string uploadPath = Server.MapPath("~/xmlFile");
            string filename = doc.ToString() + "moms.xml";
            string path = Path.Combine(uploadPath, filename);
            doc.Save(path + filename);
            Process.Start(path + filename);
            return new HttpStatusCodeResult(204);
        }
    }
}