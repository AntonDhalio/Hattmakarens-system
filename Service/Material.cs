using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
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

        public HatViewModel SetMaterials (HatViewModel model)
        {
            var materialRepo = new MaterialRepository();
            model.Statuses = new List<SelectListItem>();
            foreach (var material in materialRepo.GetAllMaterials())
            {
                var listitem = new SelectListItem
                {
                    Value = material.Id.ToString(),
                    Text = material.Name + ", " + material.Color.Name + ", " + material.Type
                };
                model.Statuses.Add(listitem);
            }

            var SelectedMaterialsId = materialRepo.GetMaterialInHatmodel(model.HatModelName);
            model.SelectedStatuses = new int[100];

            int count = 0;
            foreach (var id in SelectedMaterialsId)
            {
                model.SelectedStatuses[count] = id;
                count++;
            }
            return model;
        }

    }
}