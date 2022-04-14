using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Hattmakarens_system.Services
{
    public class TranslateService
    {
        public String Translate(String word)
        {
            var toLanguage = "en";
            var fromLanguage = "sv";
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }

        public PdfLabelsViewModel TranslatePdf(PdfLabelsViewModel swedish)
        {
            PdfLabelsViewModel english = new PdfLabelsViewModel
            {
                Invoice = Translate(swedish.Shipping),
                To = Translate(swedish.To),
                From = Translate(swedish.From),
                Customer = Translate(swedish.Customer),
                Company = Translate(swedish.Company),
                Content = Translate(swedish.Content),
                Weight = Translate(swedish.Weight),
                Shipping = Translate(swedish.Shipping),
                ShippingCode = Translate(swedish.ShippingCode),
                Date = Translate(swedish.Date),
                OrganisationNumber = Translate(swedish.OrganisationNumber),
                Bankgiro = Translate(swedish.Bankgiro),
                CustomerName = Translate(swedish.CustomerName),
                Address = Translate(swedish.Address),
                Total = Translate(swedish.Total),
                DueDate = Translate(swedish.DueDate),
                HatName = Translate(swedish.HatName),
                Size = Translate(swedish.Size),
                Price = Translate(swedish.Price),
                OrderNr = Translate(swedish.OrderNr),
                CustomerNumber = Translate(swedish.CustomerNumber),
                Maker = Translate(swedish.Maker),
                Statistics = Translate(swedish.Statistics),
                Time = Translate(swedish.Time),
                HatAmount = Translate(swedish.HatAmount),
                OrderAmount = Translate(swedish.OrderAmount),
                OrderDate = Translate(swedish.OrderDate),
            };

            return english;
        }
    }
}