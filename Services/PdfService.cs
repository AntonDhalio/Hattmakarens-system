using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Hattmakarens_system.Services
{
    public class PdfService
    {

        //Skapa PDF-fil
        public void PdfFunktion(string viewData)
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            gfx.DrawString(viewData, font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

            string path = HttpContext.Current.Server.MapPath("~/Pdf/");

            string filename = document.Guid.ToString() + ".pdf";

            document.Save(path + filename);

            Process.Start(path + filename);
        }
    }
}