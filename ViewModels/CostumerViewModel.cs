using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class CostumerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Namn")]
        public string Name { get; set; }
        [DisplayName("Telefonnummer")]
        public int Phone { get; set; }
        [DisplayName("Epost")]
        public string Email { get; set; }
        [DisplayName("Adress")]
        public string Adress { get; set; }
        [DisplayName("Kommentar")]
        public string Comment { get; set; }
        public virtual ICollection<OrderModels> Orders { get; set; }
    }
}
