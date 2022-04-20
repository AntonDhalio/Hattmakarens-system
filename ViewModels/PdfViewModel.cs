using Hattmakarens_system.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.ViewModels
{
    public class InvoiceViewModel
    {
        public List<SelectListItem> Languages { get; set; }
        [Required]
        public string Language { get; set; }
        public OrderModels Order { get; set; }
        public CustomerModels Customer { get; set; }
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "Förfallodatum")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [Required]
        public string OCR { get; set; }
        [Required]
        [Display(Name = "Fakturanummer")]
        public int? InvoiceId { get; set; } = null;

    }

    public class ShippingViewModel
    {
        public List<SelectListItem> Languages { get; set; }
        public string Language { get; set; }
        public bool Translate { get; set; } 
        public OrderModels Order { get; set; }
        public CustomerModels Customer { get; set; }
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "Vikt  (kg)")]
        public double? Weight { get; set; }
        [Required]
        [Display(Name = "Kod (8 siffror)")]
        public int? ShippingCode { get; set; }

    }

    public class PdfLabelsViewModel
    {
        public string Invoice { get; set; } = "Faktura";
        public string Shipping { get; set; } = "Fraktsedel";
        public string Statistics { get; set; } = "Statistik";
        public string To { get; set; } = "Till";
        public string From { get; set; } = "Från";
        public string OrganisationNumber { get; set; } = "Organisationsnummer";
        public string Bankgiro { get; set; } = "Bankgiro";
        public string Customer { get; set; } = "Kund";
        public string CustomerName { get; set; } = "Kundnamn";
        public string Address { get; set; } = "Adress";
        public string Total { get; set; } = "Totalsumma";
        public string DueDate { get; set; } = "Förfallodatum";
        public string HatName { get; set; } = "Namn";
        public string Size { get; set; } = "Storlek";
        public string Price { get; set; } = "Köpesumma";
        public string Company { get; set; } = "Företag";
        public string Content { get; set; } = "Innehåll";
        public string Weight { get; set; } = "Vikt";
        public string ShippingCode { get; set; } = "Fraktkod";
        public string Date { get; set; } = "Datum";
        public string OrderNr { get; set; } = "Beställning nummer";
        public string CustomerNumber { get; set; } = "Kundnummer";
        public string Maker { get; set; } = "Hattmakare";
        public string Time { get; set; } = "Tid";
        public string HatAmount { get; set; } = "Antal hattar";
        public string OrderAmount { get; set; } = "Antal beställningar";
        public string OrderDate { get; set; } = "Beställningsdatum";
        public string Status { get; set; } = "Status";
    }
}