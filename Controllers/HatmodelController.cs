using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class HatmodelController : Controller
    {
        // GET: Hatmodel
        public ActionResult Hatmodel()
        {
            var materialRepo = new MaterialRepository();
            var model = new HatmodelViewModel
            {
                MaterialsToPickFrom = materialRepo.GetAllMaterials()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Hatmodel(HatmodelViewModel hatmodel) 
        {
            var newHatmodel = new HatModels
            {
                Name = hatmodel.Name,
                Description = hatmodel.Description,
                Price = hatmodel.Price,
                Material = hatmodel.Material
            };

            var hatRepo = new HatmodelRepository();
            hatRepo.SaveHatmodel(newHatmodel);
            var materialRepo = new MaterialRepository();
            foreach(var material in newHatmodel.Material)
            {
                materialRepo.GetMaterial(material.Id);
                material.HatModels.Add(newHatmodel);
                materialRepo.SaveMaterial(material);
            }
            
            return View();

        }
    }
}