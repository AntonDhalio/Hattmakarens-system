using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class MaterialViewModel
    {
        public int Id { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [DisplayName("Materialnamn")]
        public string Name { get; set; }
        [DisplayName("Materialtyp")]
        public string Type { get; set; }
        [DisplayName("Färg")]
        public int ColorId { get; set; }
        public virtual ColorModels Color { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
        public virtual ICollection<HatModels> HatModels { get; set; }
        public virtual List<SelectListItem> ColorsToPickFrom { get; set; }
        public string PickedColor { get; set; }

    }
}
