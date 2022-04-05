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
            var model = new MaterialViewModel
            {
                ColorsToPickFrom = colorRepo.GetAllColors()
        };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMaterial(MaterialViewModel materialViewModel)
        {
            try
            {
                var matRepo = new MaterialRepository();
                var material = new MaterialModels
                {
                    Name = materialViewModel.Name,
                    Description = materialViewModel.Description,
                    Type = materialViewModel.Type
                };
                matRepo.SaveMaterial(material);
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
    }
}