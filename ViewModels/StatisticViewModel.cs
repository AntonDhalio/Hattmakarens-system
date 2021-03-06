using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Controls;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class StatisticViewModel
    {
        public string Language { get; set; }    
        public List<OrderModels> orders { get; set; }
        public String time { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        [DisplayName("Från-datum")]
        public DateTime fromDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        [DisplayName("Till-datum")]
        public DateTime toDate { get; set; }
        public double totalSum { get; set; }
        public int totalOrdersCount { get; set; }
        public int totalHatsCount { get; set; }
        [DisplayName("Ange total inköpsmoms:")]
        public double purchasedTax { get; set; }

        public List<SelectListItem> customers { get; set; }
        public string customerId { get; set; }
        
        public List<SelectListItem> hatmodels { get; set; }
        public string hatmodelId { get; set; }
        

    }
}