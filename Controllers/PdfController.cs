using Hattmakarens_system.Services;
using Hattmakarens_system.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class PdfController : Controller
    {
        PdfService pdfService = new PdfService();
        PdfTemplates pdfTemplates = new PdfTemplates();

        // PRINT: Invoice
        public ActionResult Invoice(InvoiceViewModel invoice, int id, DateTime DueDate)
        {
            invoice.DueDate = DueDate;
            invoice.Languages = PopulateLangList();
            if (ModelState.IsValid)
            {
                pdfService.PrintInvoice(invoice, id);
                return RedirectToAction("ViewOrder", "Order", new { Id = id });
            }

            ModelState.Clear();
            return View(invoice);
        }

        // PRINT: Shipping
        public ActionResult Shipping(ShippingViewModel shipping, int id)
        {
            shipping.Languages = PopulateLangList();
            
            if (ModelState.IsValid)
            {
                pdfService.PrintShipping(shipping, id);
                return RedirectToAction("ViewOrder", "Order", new { Id = id });
            }

            ModelState.Clear();
            return View(shipping);
        }

        public ActionResult Order(int id)
        {
            pdfTemplates.OrderPDF(id);
            return RedirectToAction("ViewOrder", "Order", new { Id = id });
        }

        public GoogleLanguages GoogleLangList()
        {
            string json = new System.Net.WebClient().DownloadString("https://raw.githubusercontent.com/itsecurityco/to-google-translate/master/supported_languages.json");
            GoogleLanguages languages = JsonConvert.DeserializeObject<GoogleLanguages>(json);
            return languages;
        }

        public List<SelectListItem> PopulateLangList()
        {
            List<SelectListItem> languages = new List<SelectListItem>();
            var googlelist = GoogleLangList();

            foreach (var language in googlelist.text)
            {
                if (language.code.Equals("sv"))
                {
                    languages.Add(new SelectListItem
                    {
                        Value = language.code,
                        Text = language.language,
                        Selected = true
                    });
                }
                new SelectListItem { Value = language.code, Text = language.language };
                languages.Add(new SelectListItem
                {
                    Value = language.code,
                    Text = language.language
                });
            }

            return languages;
        }
    }
}