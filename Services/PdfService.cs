using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Web;
using Hattmakarens_system.Models;
using System.Collections.Generic;
using System;

namespace Hattmakarens_system.Services
{
    public class PdfService
    {

        //Faktura PDF
        public void InvoicePDF()
        {

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont headerFont = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont contentFont = new XFont("Verdana", 10, XFontStyle.Regular);
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

            // Create rectangle.
            XRect rect = new XRect(30, 200, 300, 150);

            gfx.DrawRectangle(XBrushes.LightGray, rect);
            gfx.DrawString("Kundnamn: " , contentFont, XBrushes.Black,
            50, 230);
            gfx.DrawString("Adress: " , contentFont, XBrushes.Black,
            50, 260);
            gfx.DrawString("OCR: " , contentFont, XBrushes.Black,
            50, 290);
            gfx.DrawString("Totalsumma:  kr", contentFont, XBrushes.Black,
            50, 320);

            string path = HttpContext.Current.Server.MapPath("~/App_Data/");
            string filename = document.Guid.ToString() + "faktura.pdf";

            document.Save(path + filename);
            Process.Start(path + filename);
        }

        //Fraktsedel PDF
        public void ShippingPDF()
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
            gfx.DrawString("Mathilda", contentFont, XBrushes.Black,
            240, 170);
            //Adress
            gfx.DrawString("Adress", contentFontBold, XBrushes.Black,
            180, 190);
            gfx.DrawString("Gatans gata3239", contentFont, XBrushes.Black,
            240, 190);
            gfx.DrawString("9420 Öbro", contentFont, XBrushes.Black,
            240, 210);

            //Företagsinfo
            gfx.DrawString("Från", contentFontItalic, XBrushes.Black,
            180, 350);
            //Företag
            gfx.DrawString("Företag", contentFontBold, XBrushes.Black,
            180, 370);
            gfx.DrawString("Hattmakaren", contentFont, XBrushes.Black,
            240, 370);
            //Adress
            gfx.DrawString("Adress", contentFontBold, XBrushes.Black,
            180, 390);
            gfx.DrawString("Öbrbro gatan 8849", contentFont, XBrushes.Black,
            240, 390);
            gfx.DrawString("67483 Örebro", contentFont, XBrushes.Black,
            240, 410);

            //Paketinfo
            gfx.DrawString("Innehåll", contentFontItalic, XBrushes.Black,
            180, 550);
            //Vikt
            gfx.DrawString("Vikt", contentFontBold, XBrushes.Black,
            180, 570);
            gfx.DrawString("8492kg", contentFont, XBrushes.Black,
            240, 570);
            //Pris
            gfx.DrawString("Pris", contentFontBold, XBrushes.Black,
            180, 590);
            gfx.DrawString("3000kr", contentFont, XBrushes.Black,
            240, 590);
            //Pris
            gfx.DrawString("Kod", contentFontBold, XBrushes.Black,
            180, 610);
            gfx.DrawString("12345678", contentFont, XBrushes.Black,
            240, 610);
            //Varor
            gfx.DrawString("Varor", contentFont, XBrushes.Black,
            180, 630);

            //KOD FÖR HATTARNA HÄR!!!!!!!!!//

            string path = HttpContext.Current.Server.MapPath("~/App_Data/");
            string filename = document.Guid.ToString() + "frakt.pdf";

            document.Save(path + filename);
            Process.Start(path + filename);
        }

        //Beställningsinformation PDF
        public void OrderPDF()
        {
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
            XRect rect = new XRect(150, 120, 300, 150);
            gfx.DrawRectangle(XBrushes.LightGray, rect);

            //Header
            gfx.DrawString("BESTÄLLNING", headerFont, XBrushes.Black,
                new XRect(0, 50, page.Width, page.Height), XStringFormats.TopCenter);

            //Datum
            gfx.DrawString("Datum: " + DateTime.Now.Date.ToShortDateString(), miniFont, XBrushes.Black,
            180, 100, XStringFormats.Center);

            //Beställningsinfo
            gfx.DrawString("Nummer", contentFontBold, XBrushes.Black,
            180, 170);
            gfx.DrawString("8593859", contentFont, XBrushes.Black,
            300, 170);
            //Kundnamn
            gfx.DrawString("Kund", contentFontBold, XBrushes.Black,
            180, 190);
            gfx.DrawString("Gretis gris", contentFont, XBrushes.Black,
            240, 190);
            //Adress
            gfx.DrawString("Adress", contentFontBold, XBrushes.Black,
            180, 210);
            gfx.DrawString("Gatans gata3239", contentFont, XBrushes.Black,
            240, 210);
            gfx.DrawString("9420 Öbro", contentFont, XBrushes.Black,
            240, 230);

            //HATTINFO KOD HÄR!!!!!!!!!//

            string path = HttpContext.Current.Server.MapPath("~/App_Data/");
            string filename = document.Guid.ToString() + "bestallning.pdf";

            document.Save(path + filename);
            Process.Start(path + filename);
        }
    }
}