using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Windows.Controls;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class StatisticViewModel
    {
        public List<OrderModels> orders { get; set; }
        public String time { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Från-datum")]
        public DateTime fromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Till-datum")]
        public DateTime toDate { get; set; }
        public double totalSum { get; set; }
        public int totalOrdersCount { get; set; }
        public int totalHatsCount { get; set; }

    }
}