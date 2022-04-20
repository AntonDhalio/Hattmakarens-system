using Hattmakarens_system.Services;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class PdfController : Controller
    {
        PdfService pdfService = new PdfService();

        // PRINT: Invoice
        public ActionResult Invoice(InvoiceViewModel createmodel, int id)
        {
            if (ModelState.IsValid)
            {
                pdfService.PrintInvoice(createmodel, id);
            }

            ModelState.Clear();
            return View(createmodel);
        }

        // PRINT: Shipping
        public ActionResult Shipping(ShippingViewModel shipping, int id)
        {
            if (ModelState.IsValid)
            {
                pdfService.PrintShipping(shipping, id);
            }

            ModelState.Clear();
            return View(shipping);
        }
    }
}