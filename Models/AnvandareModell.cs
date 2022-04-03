using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class AnvandareModell
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<BestallningModell> Bestallingar { get; set; }
        public virtual ICollection<Hatt> Hattar { get; set; }
    }
}