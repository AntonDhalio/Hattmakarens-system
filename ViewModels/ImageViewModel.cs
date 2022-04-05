using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.ViewModels
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public virtual ICollection<HatModels> HatModels { get; set; }
        public virtual ICollection<Hats> Hats { get; set; }
    }
}
