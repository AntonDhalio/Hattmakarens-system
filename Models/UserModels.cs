using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Models
{
    public class UserModels
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<OrderModels> Orders { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
    }
}