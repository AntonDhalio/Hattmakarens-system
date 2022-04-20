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
    public class HatViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Namn på hatt")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Hattstorlek")]
        public string Size { get; set; }
        [DisplayName("Bild")]
        public string Path { get; set; }
        [Required]
        [DisplayName("Pris på hatt")]
        public double Price { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Kommentar på hatt")]
        public string Comment { get; set; }
        [Required]
        [DisplayName("Tillverkare")]
        public string UserId { get; set; }
        [DisplayName("Tillverkare")]
        public string UserName { get; set; }
        public int OrderId { get; set; }
        public virtual UserModels User { get; set; }
        public int HatModelID { get; set; }
        [DisplayName("Lagerförd hatt")]
        public string HatModelName { get; set; }
        [DisplayName("Beskrivning för lagerförd hatt ")]
        public string HatModelDescription { get; set; }
        public virtual HatModels Models { get; set; }
        public string CustomerEmail { get; set; }
        public virtual ICollection<ImageModels> Images { get; set; }
        public virtual ICollection<MaterialModels> Materials { get; set; }
        public List<SelectListItem> MaterialsToPickFrom { get; set; }
        [DisplayName("Välj material")]
        public IEnumerable<string> PickedMaterials { get; set; }
        public List<SelectListItem> Statuses { get; set; }
        [DisplayName("Välj material")]
        public int[] SelectedStatuses { get; set; }
        public virtual List<SelectListItem> UsersToPickFrom { get; set; }
    }
}
