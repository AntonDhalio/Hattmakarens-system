using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class MaterialModell
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [ForeignKey("FargModell")]
        public int FargId { get; set; }
        public virtual FargModell Farg { get; set; }
        public virtual ICollection<Hatt> Hattar { get; set; }
        public virtual ICollection<HattModeller> HattModeller { get; set; }
    }
}