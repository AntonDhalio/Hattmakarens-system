﻿using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Web;
using Hattmakarens_system.Models;
using System.Collections.Generic;
using System;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;

namespace Hattmakarens_system.Services
{
    public class PdfTemplates
    {
        OrderRepository orderRepository = new OrderRepository();
        CustomerRepository customerRepository = new CustomerRepository();

        //Faktura PDF
        public void InvoicePDF(InvoiceViewModel invoice)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont headerFont = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont contentFont = new XFont("Verdana", 10, XFontStyle.Regular);
            XFont contentFontBold = new XFont("Verdana", 10, XFontStyle.Bold);
            XFont miniFont = new XFont("Verdana", 8, XFontStyle.Italic);

            //XImage image = XImage.FromFile(@"../../Pdf/sharpit.jpg");
            gfx.DrawString("FAKTURA", headerFont, XBrushes.Black,
                new XRect(0, 50, page.Width, page.Height), XStringFormats.TopCenter);
            //gfx.DrawImage(image, 100, 100);

            gfx.DrawString("Hattmakaren", miniFont, XBrushes.Black,
            50, 130);
            gfx.DrawString("Hattgränd 34", miniFont, XBrushes.Black,
            50, 140);
            gfx.DrawString("876 65 ÖREBRO", miniFont, XBrushes.Black,
            50, 150);
            gfx.DrawString("07455684992", miniFont, XBrushes.Black,
            50, 160);
            gfx.DrawString("organisationsnummer: 5591433437", miniFont, XBrushes.Black,
            50, 170);
            gfx.DrawString("bankgiro: 85938", miniFont, XBrushes.Black,
            50, 180);

            //Skapa Rektangel
            XRect rect = new XRect(30, 200, 300, 180);
            gfx.DrawRectangle(XBrushes.LightGray, rect);

            gfx.DrawString("Kundnamn: " + invoice.Customer.Name, contentFont, XBrushes.Black,
            50, 230);
            gfx.DrawString("Adress: " + invoice.Customer.Adress, contentFont, XBrushes.Black,
            50, 260);
            gfx.DrawString("OCR: " + invoice.OCR, contentFont, XBrushes.Black,
            50, 290);
            gfx.DrawString("Totalsumma: " + invoice.Order.TotalSum, contentFont, XBrushes.Black,
            50, 320);
            gfx.DrawString("Förfallodatum: " + invoice.DueDate, contentFont, XBrushes.Black,
            50, 350);

            //Titlar
            gfx.DrawString("Namn", contentFontBold, XBrushes.Black,
            50, 400);
            gfx.DrawString("Storlek", contentFontBold, XBrushes.Black,
            150, 400);
            gfx.DrawString("Pris", contentFontBold, XBrushes.Black,
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
            gfx.DrawString("FRAKTSEDEL", headerFont, XBrushes.Black,
                new XRect(0, 50, page.Width, page.Height), XStringFormats.TopCenter);

            //Datum
            gfx.DrawString("Datum: " + DateTime.Now.Date.ToShortDateString(), miniFont, XBrushes.Black,
            180, 100, XStringFormats.Center);

            //Kundinfo
            gfx.DrawString("Till", contentFontItalic, XBrushes.Black,
            180, 150);
            //Kundnamn
            gfx.DrawString("Kund", contentFontBold, XBrushes.Black,
            180, 170);
            gfx.DrawString(shipping.Customer.Name, contentFont, XBrushes.Black,
            240, 170);
            //Adress
            gfx.DrawString("Adress", contentFontBold, XBrushes.Black,
            180, 190);
            gfx.DrawString(shipping.Customer.Adress, contentFont, XBrushes.Black,
            240, 190);

            //Företagsinfo
            gfx.DrawString("Från", contentFontItalic, XBrushes.Black,
            180, 350);
            //Företag
            gfx.DrawString("Företag", contentFontBold, XBrushes.Black,
            180, 370);
            gfx.DrawString("Hattmakaren AB", contentFont, XBrushes.Black,
            240, 370);
            //Adress
            gfx.DrawString("Adress", contentFontBold, XBrushes.Black,
            180, 390);
            gfx.DrawString("Hattmakargatan 32", contentFont, XBrushes.Black,
            240, 390);
            gfx.DrawString("702-19 ÖREBRO", contentFont, XBrushes.Black,
            240, 410);

            //Paketinfo
            gfx.DrawString("Innehåll", contentFontItalic, XBrushes.Black,
            180, 550);
            //Vikt
            gfx.DrawString("Vikt", contentFontBold, XBrushes.Black,
            180, 570);
            gfx.DrawString(shipping.Weight.ToString() + " kg", contentFont, XBrushes.Black,
            240, 570);
            //Pris
            gfx.DrawString("Pris", contentFontBold, XBrushes.Black,
            180, 590);
            gfx.DrawString(shipping.Order.TotalSum.ToString(), contentFont, XBrushes.Black,
            240, 590);
            //Pris
            gfx.DrawString("Kod", contentFontBold, XBrushes.Black,
            180, 610);
            gfx.DrawString(shipping.ShippingCode.ToString(), contentFont, XBrushes.Black,
            240, 610);

            //Titlar
            gfx.DrawString("Namn", contentFontBold, XBrushes.Black,
            150, 700);
            gfx.DrawString("Storlek", contentFontBold, XBrushes.Black,
            250, 700);
            gfx.DrawString("Pris", contentFontBold, XBrushes.Black,
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

            //Skapa rektangel (bakgrund) 1
            XRect rect = new XRect(150, 120, 300, 140);
            gfx.DrawRectangle(XBrushes.LightGray, rect);

            //Header
            gfx.DrawString("BESTÄLLNING NR " + order.Id, headerFont, XBrushes.Black,
                new XRect(0, 50, page.Width, page.Height), XStringFormats.TopCenter);

            //Datum
            gfx.DrawString("Datum: " + order.Date.ToShortDateString(), miniFont, XBrushes.Black,
            180, 100, XStringFormats.Center);

            //Kundnamn
            gfx.DrawString("Kund", contentFontBold, XBrushes.Black,
            180, 170);
            gfx.DrawString(customer.Name, contentFont, XBrushes.Black,
            240, 170);
            //Adress
            gfx.DrawString("Adress", contentFontBold, XBrushes.Black,
            180, 190);
            gfx.DrawString(customer.Adress, contentFont, XBrushes.Black,
            240, 190);
            //Email
            gfx.DrawString("Email", contentFontBold, XBrushes.Black,
            180, 210);
            gfx.DrawString(customer.Email, contentFont, XBrushes.Black,
            240, 210);
            //KundId
            gfx.DrawString("Kundnr", contentFontBold, XBrushes.Black,
            180, 230);
            gfx.DrawString(customer.Id.ToString(), contentFont, XBrushes.Black,
            240, 230);

            //Titlar
            gfx.DrawString("Namn", contentFontBold, XBrushes.Black,
            50, 300);
            gfx.DrawString("Storlek", contentFontBold, XBrushes.Black,
            150, 300);
            gfx.DrawString("Skapare", contentFontBold, XBrushes.Black,
            250, 300);
            gfx.DrawString("Status", contentFontBold, XBrushes.Black,
            350, 300);
            gfx.DrawString("Pris", contentFontBold, XBrushes.Black,
            450, 300);

            int x = 320;

            foreach (var hat in order.Hats)
            {
                gfx.DrawString(hat.Name, contentFont, XBrushes.Black,
                50, x);
                gfx.DrawString(hat.Size, contentFont, XBrushes.Black,
                150, x);
                gfx.DrawString("mig", contentFont, XBrushes.Black,
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
    }
}