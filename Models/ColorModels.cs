using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class ColorModels
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MaterialModels> Material { get; set; }
    }
}