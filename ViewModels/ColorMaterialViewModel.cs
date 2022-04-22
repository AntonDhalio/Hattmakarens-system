using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.ViewModels
{
    public class ColorMaterialViewModel
    {
        public int Id { get; set; }
        [DisplayName("Färgnamn")]
        public string Name { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool State { get; set; }
    }
}