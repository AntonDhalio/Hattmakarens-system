using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.ViewModels
{
    public class SearchViewModel
    {
        public int CustomerId { get; set; }
        [DisplayName("Namn")]
        public string CustomerName { get; set; }
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string MaterialName { get; set; }
        public string HatModelName { get; set; }

    }
}