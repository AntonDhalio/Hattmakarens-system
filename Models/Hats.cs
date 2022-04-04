using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class Hats
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        [ForeignKey("UserModels")]
        public string UserId { get; set; }
        public virtual UserModels User { get; set; }
        [ForeignKey("HatModels")]
        public int ModelID { get; set; }
        public virtual HatModels Models { get; set; }
        public virtual ICollection<ImageModels> Images { get; set; }


    }
}