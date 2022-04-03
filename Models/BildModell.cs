using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class BildModell
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public virtual ICollection<HattModeller> HattModeller { get; set; }
        public virtual ICollection<Hatt> Hattar { get; set; }

    }
}