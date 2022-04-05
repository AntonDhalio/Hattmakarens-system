using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Threading.Tasks;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [DisplayName("Namn")]
        public string Name { get; set; }
        [DisplayName("Lösenord")]
        public string Password { get; set; }
        public virtual ICollection<OrderModels> Orders { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
    }
}