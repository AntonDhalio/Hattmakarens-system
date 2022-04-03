using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class KundModell
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Comment { get; set; }
        public virtual ICollection<BestallningModell> Bestallningar { get; set; }

    }
}