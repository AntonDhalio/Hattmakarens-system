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
        [DisplayName("Namn")]
        public string Name { get; set; }
        [DisplayName("Storlek")]
        public string Size { get; set; }
        [DisplayName("Pris")]
        public double Price { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Kommentar")]
        public string Comment { get; set; }
        public string UserId { get; set; }
        public virtual UserModels User { get; set; }
        public int ModelID { get; set; }
        public virtual HatModels Models { get; set; }
        public virtual ICollection<ImageModels> Images { get; set; }
    }
}
