using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class ImageModels
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public virtual ICollection<HatModels> HatModels { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }

    }
}