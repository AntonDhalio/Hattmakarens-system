using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class StatisticViewModel
    {
        public List<OrderModels> orders { get; set; }
        public String time { get; set; }
        public double totalSum { get; set; }
        public int totalOrdersCount { get; set; }
        public int totalHatsCount { get; set; }

    }
}