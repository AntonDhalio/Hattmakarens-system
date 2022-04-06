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
    public class MaterialController : Controller
    {
        // GET: Material
        public ActionResult AddMaterial()
        {
            var colorRepo = new ColorRepository();
            var model = new MaterialViewModel();
            model.ColorsToPickFrom = new List<SelectListItem>();
            foreach (var color in colorRepo.GetAllColors())
            {
                var listitem = new SelectListItem
                {
                    Text = color.Name,
                    Value = color.Id.ToString()
                };
                model.ColorsToPickFrom.Add(listitem);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMaterial(MaterialViewModel materialViewModel)
        {
            //try
            //{
                var matRepo = new MaterialRepository();
                var material = new MaterialModels
                {
                    Name = materialViewModel.Name,
                    Description = materialViewModel.Description,
                    Type = materialViewModel.Type,
                    ColorId = int.Parse(materialViewModel.PickedColor.Value)
                };
                matRepo.SaveMaterial(material);
                return View();
            //}
            //catch
            //{
              //  return View("Error");
            //}
        }
    }
}