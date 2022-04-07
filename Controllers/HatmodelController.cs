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
        public ActionResult Hatmodel(HatmodelViewModel hatmodel, IEnumerable<string> PickedMaterials) 
        {
            var newHatmodel = new HatModels
            {
                Name = hatmodel.Name,
                Description = hatmodel.Description,
                Price = hatmodel.Price,
                Material = new List<MaterialModels>()
            };
            using (var context = new ApplicationDbContext())
            {
                foreach (var material in PickedMaterials)
                {
                    var id = int.Parse(material);
                    var aMaterial = context.Material.ToList().FirstOrDefault(m => m.Id == id);
                    newHatmodel.Material.Add(aMaterial);
                }
                context.HatModels.Add(newHatmodel);
                context.SaveChanges();
            }

            return RedirectToAction("Hatmodel", "Hatmodel");

        }
    }
}