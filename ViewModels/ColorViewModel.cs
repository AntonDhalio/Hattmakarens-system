using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class ColorViewModel
    {
        public int Id { get; set; }
        [DisplayName("Färgnamn")]
        public string Name { get; set; }
        //public virtual ICollection<MaterialModels> Material { get; set; }
    }
}
