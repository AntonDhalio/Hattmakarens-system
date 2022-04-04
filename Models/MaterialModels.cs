using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class MaterialModels
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [ForeignKey("Color")]
        public int ColorId { get; set; }
        public virtual ColorModels Color { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
        public virtual ICollection<HatModels> HatModels { get; set; }
    }
}