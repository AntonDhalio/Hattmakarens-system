using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class BestallningModell
    {
        [Key]
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public bool Priority { get; set; }
        public string Status { get; set; }
        public double Moms { get; set; }
        public double TotalSum { get; set; }
        public string Comment { get; set; }
        [ForeignKey("AnvandareModell")]
        public string UserId { get; set; }
        public virtual AnvandareModell User { get; set; }
        [ForeignKey("KundModell")]
        public int CustomerId { get; set; }
        public virtual KundModell Customer { get; set; }
        public virtual ICollection<Hatt> Hattar { get; set; }
        

    }
}