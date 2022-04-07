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
            //var model = new HatmodelViewModel
            //{
            //    MaterialsToPickFrom = new List<SelectListItem>()
            //};
            var materials = new List<SelectListItem>();
            foreach(var material in materialRepo.GetAllMaterials())
            {
                var listitem = new SelectListItem
                {
                    Value = material.Id.ToString(),
                    Text = material.Name
                };
                materials.Add(listitem);
            }
            ViewBag.MaterialsToPickFrom = materials;
            return View();
        }
        [HttpPost]
        public ActionResult Hatmodel(HatmodelViewModel hatmodel) 
        {
            var newHatmodel = new HatModels
            {
                Name = hatmodel.Name,
                Description = hatmodel.Description,
                Price = hatmodel.Price,
                Material = new List<MaterialModels>()
            };
            var hatRepo = new HatmodelRepository();
            hatRepo.SaveHatmodel(newHatmodel);
            var lastHatmodel = hatRepo.GetHatmodel(hatRepo.GetAllHatmodels().LastOrDefault().Id);
            var materialRepo = new MaterialRepository();
            //foreach (var material in Request.Form["Material"])
            //{
                var aMaterial = materialRepo.GetMaterial(int.Parse(Request.Form["Material"]));
                lastHatmodel.Material.Add(aMaterial);
            //}
            hatRepo.SaveHatmodel(lastHatmodel);
            var theHat = hatRepo.GetHatmodel(lastHatmodel.Id);
            foreach (var material in theHat.Material)
            {
                var mat = materialRepo.GetMaterial(material.Id);
                mat.HatModels.Add(theHat);
                materialRepo.SaveMaterial(mat);
            }

            return RedirectToAction("Hatmodel", "Hatmodel");

        }
    }
}