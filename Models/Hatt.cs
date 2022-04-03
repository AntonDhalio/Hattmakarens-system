using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class Hatt
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        [ForeignKey("AnvandareModell")]
        public string UserId { get; set; }
        public virtual AnvandareModell User { get; set; }
        [ForeignKey("HattModeller")]
        public int ModelID { get; set; }
        public virtual HattModeller Modeller { get; set; }
        public virtual ICollection<BildModell> Bilder { get; set; }


    }
}