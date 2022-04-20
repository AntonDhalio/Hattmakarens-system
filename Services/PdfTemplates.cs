using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Web;
using Hattmakarens_system.Models;
using System.Collections.Generic;
using System;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using Hattmakarens_system.Services;

namespace Hattmakarens_system.Services
{
    public class PdfTemplates
    {
        OrderRepository orderRepository = new OrderRepository();
        CustomerRepository customerRepository = new CustomerRepository();
        PdfLabelsViewModel labels = new PdfLabelsViewModel();

        //Faktura PDF
        public void InvoicePDF(InvoiceViewModel invoice)
        {
            TranslateService ts = new TranslateService(invoice.Language);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont headerFont = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont contentFont = new XFont("Verdana", 10, XFontStyle.Regular);
            XFont contentFontBold = new XFont("Verdana", 10, XFontStyle.Bold);
            XFont miniFont = new XFont("Verdana", 8, XFontStyle.Italic);

            if (!invoice.Language.Equals("sv") || invoice.Language != null)
            {
                labels = ts.TranslatePdf(labels);
            }

            gfx.DrawString(labels.Invoice, headerFont, XBrushes.Black,
                new XRect(0, 50, page.Width, page.Height), XStringFormats.TopCenter);

            gfx.DrawString("Hattmakaren", miniFont, XBrushes.Black,
            50, 130);
            gfx.DrawString("Hattgränd 34", miniFont, XBrushes.Black,
            50, 140);
            gfx.DrawString("876 65 ÖREBRO", miniFont, XBrushes.Black,
            50, 150);
            gfx.DrawString("07455684992", miniFont, XBrushes.Black,
            50, 160);
            gfx.DrawString(labels.OrganisationNumber + ": 559143-3437", miniFont, XBrushes.Black,
            50, 170);
            gfx.DrawString(labels.Bankgiro + ": 85938", miniFont, XBrushes.Black,
            50, 180);

            //Skapa Rektangel
            XRect rect = new XRect(30, 200, 300, 180);
            gfx.DrawRectangle(XBrushes.LightGray, rect);

            gfx.DrawString(labels.CustomerName + ": " + invoice.Customer.Name, contentFont, XBrushes.Black,
            50, 230);
            gfx.DrawString(labels.Address + ": " + invoice.Customer.Adress, contentFont, XBrushes.Black,
            50, 260);
            gfx.DrawString("OCR: " + invoice.OCR, contentFont, XBrushes.Black,
            50, 290);
            gfx.DrawString(labels.Total + ": " + invoice.Order.TotalSum, contentFont, XBrushes.Black,
            50, 320);
            gfx.DrawString(labels.DueDate + ": " + invoice.DueDate.ToShortDateString(), contentFont, XBrushes.Black,
            50, 350);

            //Titlar
            gfx.DrawString(labels.HatName, contentFontBold, XBrushes.Black,
            50, 400);
            gfx.DrawString(labels.Size, contentFontBold, XBrushes.Black,
            150, 400);
            gfx.DrawString(labels.Price, contentFontBold, XBrushes.Black,
            250, 400);

            int x = 420;

            foreach (var hat in invoice.Order.Hats)
            {
                gfx.DrawString(hat.Name, contentFont, XBrushes.Black,
                50, x);
                gfx.DrawString(hat.Size, contentFont, XBrushes.Black,
                150, x);
                gfx.DrawString(hat.Price.ToString(), contentFont, XBrushes.Black,
                250, x);

                x += 20;
            }

            string path = HttpContext.Current.Server.MapPath("~/App_Data/");
            string filename = document.Guid.ToString() + "faktura.pdf";

            document.Save(path + filename);
            Process.Start(path + filename);
        }



        //Fraktsedel PDF
        public void ShippingPDF(ShippingViewModel shipping)
        {
            TranslateService ts = new TranslateService(shipping.Language);

            //Skapa dokument
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //Fonts
            XFont headerFont = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont contentFont = new XFont("Verdana", 10, XFontStyle.Regular);
            XFont contentFontItalic = new XFont("Verdana", 10, XFontStyle.Italic);
            XFont contentFontBold = new XFont("Verdana", 10, XFontStyle.Bold);
            XFont miniFont = new XFont("Verdana", 8, XFontStyle.Italic);

            if (!shipping.Language.Equals("sv") || shipping.Language != null)
            {
                labels = ts.TranslatePdf(labels);
            }

            //Skapa rektangel (bakgrund) 1
            XRect rect = new XRect(150, 120, 300, 150);
            gfx.DrawRectangle(XBrushes.LightGray, rect);

            //Skapa rektangel (bakgrund) 2
            XRect rect2 = new XRect(150, 320, 300, 150);
            gfx.DrawRectangle(XBrushes.LightGray, rect2);

            //Skapa rektangel (bakgrund) 3
            XRect rect3 = new XRect(150, 520, 300, 150);
            gfx.DrawRectangle(XBrushes.LightGray, rect3);

            //Header
            gfx.DrawString(labels.Shipping, headerFont, XBrushes.Black,
                new XRect(0, 50, page.Width, page.Height), XStringFormats.TopCenter);

            //Datum
            gfx.DrawString(labels.Date + ": " + DateTime.Now.Date.ToShortDateString(), miniFont, XBrushes.Black,
            180, 100, XStringFormats.Center);

            //Kundinfo
            gfx.DrawString(labels.To, contentFontItalic, XBrushes.Black,
            180, 150);
            //Kundnamn
            gfx.DrawString(labels.Customer, contentFontBold, XBrushes.Black,
            180, 170);
            gfx.DrawString(shipping.Customer.Name, contentFont, XBrushes.Black,
            240, 170);
            //Adress
            gfx.DrawString(labels.Address, contentFontBold, XBrushes.Black,
            180, 190);
            gfx.DrawString(shipping.Customer.Adress, contentFont, XBrushes.Black,
            240, 190);

            //Företagsinfo
            gfx.DrawString(labels.From, contentFontItalic, XBrushes.Black,
            180, 350);
            //Företag
            gfx.DrawString(labels.Company, contentFontBold, XBrushes.Black,
            180, 370);
            gfx.DrawString("Hattmakaren AB", contentFont, XBrushes.Black,
            240, 370);
            //Adress
            gfx.DrawString(labels.Address, contentFontBold, XBrushes.Black,
            180, 390);
            gfx.DrawString("Hattmakargatan 32", contentFont, XBrushes.Black,
            240, 390);
            gfx.DrawString("702-19 ÖREBRO", contentFont, XBrushes.Black,
            240, 410);

            //Paketinfo
            gfx.DrawString(labels.Content, contentFontItalic, XBrushes.Black,
            180, 550);
            //Vikt
            gfx.DrawString(labels.Weight, contentFontBold, XBrushes.Black,
            180, 570);
            gfx.DrawString(shipping.Weight.ToString() + "(kg)", contentFont, XBrushes.Black,
            300, 570);
            //Pris
            gfx.DrawString(labels.Price, contentFontBold, XBrushes.Black,
            180, 590);
            gfx.DrawString(shipping.Order.TotalSum.ToString() + " SEK", contentFont, XBrushes.Black,
            300, 590);
            //Pris
            gfx.DrawString(labels.ShippingCode, contentFontBold, XBrushes.Black,
            180, 610);
            gfx.DrawString(shipping.ShippingCode.ToString(), contentFont, XBrushes.Black,
            300, 610);

            //Titlar
            gfx.DrawString(labels.HatName, contentFontBold, XBrushes.Black,
            150, 700);
            gfx.DrawString(labels.Size, contentFontBold, XBrushes.Black,
            250, 700);
            gfx.DrawString(labels.Price + " (SEK)", contentFontBold, XBrushes.Black,
            350, 700);

            int x = 720;

            foreach (var hat in shipping.Order.Hats)
            {
                gfx.DrawString(hat.Name, contentFont, XBrushes.Black,
                150, x);
                gfx.DrawString(hat.Size, contentFont, XBrushes.Black,
                250, x);
                gfx.DrawString(hat.Price.ToString(), contentFont, XBrushes.Black,
                350, x);

                x += 20;
            }

            string path = HttpContext.Current.Server.MapPath("~/App_Data/");
            string filename = document.Guid.ToString() + "frakt.pdf";

            document.Save(path + filename);
            Process.Start(path + filename);
        }

        //Beställningsinformation PDF
        public void OrderPDF(int id)
        {
            var order = orderRepository.GetOrder(id);
            var customer = customerRepository.GetCustomer(order.CustomerId);

            //Skapandet av dokumentet
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //Fonts
            XFont headerFont = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont contentFont = new XFont("Verdana", 10, XFontStyle.Regular);
            XFont contentFontItalic = new XFont("Verdana", 10, XFontStyle.Italic);
            XFont contentFontBold = new XFont("Verdana", 10, XFontStyle.Bold);
            XFont miniFont = new XFont("Verdana", 8, XFontStyle.Italic);

            //if (!invoice.Language.Equals("sv") || invoice.Language != null)
            //{
            //    labels = ts.TranslatePdf(labels);
            //}

            //Skapa rektangel (bakgrund) 1
            XRect rect = new XRect(150, 120, 300, 140);
            gfx.DrawRectangle(XBrushes.LightGray, rect);

            //Header
            gfx.DrawString(labels.OrderNr + " " + order.Id, headerFont, XBrushes.Black,
                new XRect(0, 50, page.Width, page.Height), XStringFormats.TopCenter);

            //Datum
            gfx.DrawString(labels.Date + ": " + order.Date.ToShortDateString(), miniFont, XBrushes.Black,
            180, 100, XStringFormats.Center);

            //Kundnamn
            gfx.DrawString(labels.Customer, contentFontBold, XBrushes.Black,
            180, 170);
            gfx.DrawString(customer.Name, contentFont, XBrushes.Black,
            240, 170);
            //Adress
            gfx.DrawString(labels.Address, contentFontBold, XBrushes.Black,
            180, 190);
            gfx.DrawString(customer.Adress, contentFont, XBrushes.Black,
            240, 190);
            //Email
            gfx.DrawString("Email", contentFontBold, XBrushes.Black,
            180, 210);
            gfx.DrawString(customer.Email, contentFont, XBrushes.Black,
            240, 210);
            //KundId
            gfx.DrawString(labels.CustomerNumber, contentFontBold, XBrushes.Black,
            180, 230);
            gfx.DrawString(customer.Id.ToString(), contentFont, XBrushes.Black,
            240, 230);

            //Titlar
            gfx.DrawString(labels.HatName, contentFontBold, XBrushes.Black,
            50, 300);
            gfx.DrawString(labels.Size, contentFontBold, XBrushes.Black,
            150, 300);
            gfx.DrawString(labels.Maker, contentFontBold, XBrushes.Black,
            250, 300);
            gfx.DrawString(labels.Status, contentFontBold, XBrushes.Black,
            350, 300);
            gfx.DrawString(labels.Price, contentFontBold, XBrushes.Black,
            450, 300);

            int x = 320;

            foreach (var hat in order.Hats)
            {
                gfx.DrawString(hat.Name, contentFont, XBrushes.Black,
                50, x);
                gfx.DrawString(hat.Size, contentFont, XBrushes.Black,
                150, x);
                gfx.DrawString(hat.User.Name, contentFont, XBrushes.Black,
                250, x);
                gfx.DrawString(hat.Status, contentFont, XBrushes.Black,
                350, x);
                gfx.DrawString(hat.Price.ToString(), contentFont, XBrushes.Black,
                450, x);

                x += 20;
            }

            string path = HttpContext.Current.Server.MapPath("~/App_Data/");
            string filename = document.Guid.ToString() + "bestallning.pdf";

            document.Save(path + filename);
            Process.Start(path + filename);
        }

        //Statistik PDF
        public void StatisticsPDF(StatisticViewModel statistics)
        {
            TranslateService ts = new TranslateService(statistics.Language);

            if (!statistics.Language.Equals("sv") || statistics.Language != null)
            {
                labels = ts.TranslatePdf(labels);
            }

            List<OrderModels> orders = statistics.orders;
            foreach (var order in orders)
            {
                order.Hats = orderRepository.GetOrder(order.Id).Hats;
            }

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont headerFont = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont contentFont = new XFont("Verdana", 10, XFontStyle.Regular);
            XFont contentFontBold = new XFont("Verdana", 10, XFontStyle.Bold);
            XFont miniFont = new XFont("Verdana", 8, XFontStyle.Italic);

            gfx.DrawString(labels.Statistics, headerFont, XBrushes.Black,
                new XRect(0, 50, page.Width, page.Height), XStringFormats.TopCenter);

            gfx.DrawString("Hattmakaren", miniFont, XBrushes.Black,
            50, 130);
            gfx.DrawString("Hattgränd 34", miniFont, XBrushes.Black,
            50, 140);
            gfx.DrawString("876 65 ÖREBRO", miniFont, XBrushes.Black,
            50, 150);
            gfx.DrawString("07455684992", miniFont, XBrushes.Black,
            50, 160);
            gfx.DrawString(labels.OrganisationNumber + ": 5591433437", miniFont, XBrushes.Black,
            50, 170);

            //Titlar
            //Tid
            gfx.DrawString(labels.Time, contentFontBold, XBrushes.Black,
            50, 210);
            gfx.DrawString(statistics.fromDate.ToShortDateString().ToString() + " - " + statistics.toDate.ToShortDateString().ToString(), contentFont, XBrushes.Black,
            240, 210);
            //Summa
            gfx.DrawString(labels.Total, contentFontBold, XBrushes.Black,
            50, 230);
            gfx.DrawString(statistics.totalSum.ToString(), contentFont, XBrushes.Black,
            240, 230);
            //Antal hattar
            gfx.DrawString(labels.HatAmount, contentFontBold, XBrushes.Black,
            50, 250);
            gfx.DrawString(statistics.totalHatsCount.ToString(), contentFont, XBrushes.Black,
            240, 250);
            //Antal beställningar
            gfx.DrawString(labels.OrderAmount, contentFontBold, XBrushes.Black,
            50, 270);
            gfx.DrawString(statistics.totalOrdersCount.ToString(), contentFont, XBrushes.Black,
            240, 270);

            //Titlar
            gfx.DrawString(labels.OrderNr, contentFontBold, XBrushes.Black,
            50, 310);
            gfx.DrawString(labels.HatAmount, contentFontBold, XBrushes.Black,
            200, 310);
            gfx.DrawString(labels.OrderDate, contentFontBold, XBrushes.Black,
            350, 310);
            gfx.DrawString(labels.Total + " (SEK)", contentFontBold, XBrushes.Black,
            500, 310);

            int x = 340;

            foreach (var order in statistics.orders)
            {
                gfx.DrawString(order.Id.ToString(), contentFont, XBrushes.Black,
                50, x);
                gfx.DrawString(order.Hats.Count.ToString(), contentFont, XBrushes.Black,
                200, x);
                gfx.DrawString(order.Date.ToShortDateString(), contentFont, XBrushes.Black,
                350, x);
                gfx.DrawString(order.TotalSum.ToString(), contentFont, XBrushes.Black,
                500, x);

                x += 20;
            }

            string path = HttpContext.Current.Server.MapPath("~/App_Data/");
            string filename = document.Guid.ToString() + "faktura.pdf";

            document.Save(path + filename);
            Process.Start(path + filename);
        }
    }
}