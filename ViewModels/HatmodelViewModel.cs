using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [DisplayName("Modellnamn")]
        public string Name { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Pris")]
        public double Price { get; set; }
        public int OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
        public virtual ICollection<ImageModels> Images { get; set; }
        public virtual ICollection<MaterialModels> Material { get; set; }
        public List<SelectListItem> MaterialsToPickFrom { get; set; }
        public List<ColorMaterialViewModel> TygMaterial { get; set; }
        public List<ColorMaterialViewModel> DekorationMaterial { get; set; }
        public List<ColorMaterialViewModel> TrådMaterial { get; set; }
        [Required]
        [DisplayName("Välj material")]
        public IEnumerable<string> PickedMaterials { get; set; }
    }
}
