using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Mvc;


namespace Hattmakarens_system.Services
{
    public class XmlService
    {

        public void TaxXml (double inTax, double taxFromOrdersCount, double result, string uploadPath)
        {
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
            XmlText subResult = doc.CreateTextNode(result.ToString());
            element1.AppendChild(element2);
            element1.AppendChild(element3);
            element1.AppendChild(element4);
            element1.AppendChild(element5);
            element2.AppendChild(doc.CreateTextNode("Moms"));
            element3.AppendChild(purchased);
            element4.AppendChild(taxFromOrders);
            element5.AppendChild(subResult);
            string filename = doc.ToString() + "moms.xml";
            string path = Path.Combine(uploadPath, filename);
            doc.Save(path + filename);
            Process.Start(path + filename);
        }
    }
}