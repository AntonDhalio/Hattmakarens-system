using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class HatModels
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
        public virtual ICollection<ImageModels> Images { get; set; }
        public virtual ICollection<MaterialModels> Material { get; set; }
    }
}