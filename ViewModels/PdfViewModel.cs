using Hattmakarens_system.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.ViewModels
{
    public class InvoiceViewModel
    {
        public OrderModels Order { get; set; }
        public CustomerModels Customer { get; set; }
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "Förfallodatum")]
        public DateTime? DueDate { get; set; } = null;
        [Required]
        public string OCR { get; set; }
        [Required]
        [Display(Name = "Fakturanummer")]
        public int? InvoiceId { get; set; } = null;

    }

    public class ShippingViewModel
    {
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
}