using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class FargModell
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MaterialModell> Material { get; set; }

    }
}