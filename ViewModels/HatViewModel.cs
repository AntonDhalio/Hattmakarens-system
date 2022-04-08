using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class HatViewModel
    {
        public int Id { get; set; }
        [DisplayName("Namn på hatt")]
        public string Name { get; set; }
        [DisplayName("Hattstorlek")]
        public string Size { get; set; }
        [DisplayName("Bild")]
        public string Path { get; set; }
        [DisplayName("Pris på hatt")]
        public double Price { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Kommentar på hatt")]
        public string Comment { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public virtual UserModels User { get; set; }
        public int ModelID { get; set; }
        public virtual HatModels Models { get; set; }
        public string CustomerEmail { get; set; }
        public virtual ICollection<ImageModels> Images { get; set; }
        public virtual ICollection<MaterialModels> Materials { get; set; } 
    }
}
