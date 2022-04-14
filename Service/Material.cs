using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Service
{
    public class Material
    {
        public List<SelectListItem> GetSelectListMaterials()
        {
            var materialRepo = new MaterialRepository();
            var materials = new List<SelectListItem>();
            foreach (var material in materialRepo.GetAllMaterials())
            {
                var listitem = new SelectListItem
                {
                    Value = material.Id.ToString(),
                    Text = material.Name + ", " + material.Color.Name + ", " + material.Type
                };
                materials.Add(listitem);
            }
            return materials;
        }
    }
}