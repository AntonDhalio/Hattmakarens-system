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
        TranslateService translateService = new TranslateService();

        // PRINT: Invoice
        public ActionResult Invoice(InvoiceViewModel createmodel)
        {
            int id = 4;
            createmodel.Languages = PopulateLangList();

            if (ModelState.IsValid)
            {
                pdfService.PrintInvoice(createmodel, id);
            }

            ModelState.Clear();
            return View(createmodel);
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

        // PRINT: Shipping
        public ActionResult Shipping(ShippingViewModel shipping)
        {
            int id = 4;

            if (ModelState.IsValid)
            {
                pdfService.PrintShipping(shipping, id);
            }

            ModelState.Clear();
            return View(shipping);
        }
    }
}