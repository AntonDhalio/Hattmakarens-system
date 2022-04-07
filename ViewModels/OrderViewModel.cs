using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [DisplayName("Datum")]
        //[DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DisplayName("Expresstillverkning på beställning")]
        public bool Priority { get; set; }
        //[DisplayName("Status")]
        //public string Status { get; set; }
        //[DisplayName("Moms")]
        //public double Moms { get; set; }
        //[DisplayName("Totalsumma")]
        //public double TotalSum { get; set; }
        //[DisplayName("Kommentar")]
        //[DisplayName("Storlek")]
        //public double Size { get; set; }
        [DisplayName("Kommentar på beställning")]
        public string Comment { get; set; }
        //public string UserId { get; set; }
        //public virtual UserModels User { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        //public virtual CustomerModels Customer { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
      
    }
}
