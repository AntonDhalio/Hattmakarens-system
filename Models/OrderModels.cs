using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class OrderModels
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool Priority { get; set; }
        public string Status { get; set; }
        public double Moms { get; set; }
        public double TotalSum { get; set; }
        public string Comment { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual UserModels User { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual CustomerModels Customer { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
        

    }
}