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
    public class HatmodelViewModel
    {
        public int Id { get; set; }
        [DisplayName("Modellnamn")]
        public string Name { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [DisplayName("Pris")]
        public double Price { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
        public virtual ICollection<ImageModels> Images { get; set; }
        public virtual ICollection<MaterialModels> Material { get; set; }
        public List<SelectListItem> MaterialsToPickFrom { get; set; }
        public List<SelectListItem> PickedMaterials { get; set; }
    }
}
