using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Moms { get; set; }
        public string Comment { get; set; }


    }
}