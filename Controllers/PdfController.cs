using Hattmakarens_system.Services;
using Hattmakarens_system.ViewModels;
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
        public ActionResult Invoice(InvoiceViewModel invoice, int id)
        {
            invoice.Languages = PopulateLangList();
            

            if (ModelState.IsValid)
            {
                pdfService.PrintInvoice(invoice, id);
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
            }

            ModelState.Clear();
            return View(shipping);
        }

        public void Order(int id)
        {
            pdfTemplates.OrderPDF(id);
        }

        public List<SelectListItem> PopulateLangList()
        {
            List<SelectListItem> languages = new List<SelectListItem>();
            var cu = CultureInfo.GetCultures(CultureTypes.NeutralCultures);

            foreach (CultureInfo cul in cu)
            {
                new SelectListItem { Value = cul.TwoLetterISOLanguageName, Text = cul.EnglishName };
                languages.Add(new SelectListItem
                {
                    Value = cul.TwoLetterISOLanguageName,
                    Text = cul.EnglishName
                });
            }

            return languages;
        }
    }
}